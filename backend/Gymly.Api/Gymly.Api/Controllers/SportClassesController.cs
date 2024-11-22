using AutoMapper;
using Gymly.Business.Abstractions;
using Gymly.Core.Models;
using Gymly.Shared.Requests.SportClass;
using Gymly.Shared.Responses.SportClass;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Gymly.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SportClassesController : ControllerBase
{
    private readonly ISportClassRepository _sportClassRepository;
    private readonly IMapper _mapper;
    public SportClassesController(ISportClassRepository sportClassRepository, IMapper mapper)
    {
        _sportClassRepository = sportClassRepository;
        _mapper = mapper;
    }

    [HttpGet("active")]
    public async Task<IActionResult> GetAll(CancellationToken ct)
    {
        var date = DateTime.Now;
        var classes = await _sportClassRepository.GetAll(ct, date);
        if (!classes.IsSuccessful)
        {
            return BadRequest(classes.Code);
        }

        var mappedClasses = _mapper.Map<IEnumerable<SportClass>, IEnumerable<ExtendedSportClassResponse>>(classes.Data);

        return Ok(mappedClasses);
    }

    //todo all active classes for member with IsPaid = true or not

    [HttpPost]
    public async Task<IActionResult> Create([FromBody]CreateSportClassRequest sportClass, CancellationToken ct)
    {
        var mappedClass = _mapper.Map<CreateSportClassRequest, SportClass>(sportClass);
        var createdClass = await _sportClassRepository.Create(mappedClass, ct);
        if (!createdClass.IsSuccessful)
        {
            return BadRequest(createdClass.Code);
        }

        return Ok();
    }
}
