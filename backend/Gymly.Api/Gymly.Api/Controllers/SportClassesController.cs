using AutoMapper;
using Gymly.Business.Abstractions;
using Gymly.Core.Models;
using Gymly.Shared.Requests.SportClass;
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

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken ct)
    {
        var classes = await _sportClassRepository.GetAll(ct); // whre date > now
        return Ok(classes);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody]CreateSportClassRequest sportClass, CancellationToken ct)
    {
        var mappedClass = _mapper.Map<CreateSportClassRequest, SportClass>(sportClass);
        var createdClass = await _sportClassRepository.Create(mappedClass, ct);
        return Ok(createdClass);
    }

    [HttpGet("{id}/paymentsAmount")]
    public async Task<IActionResult> GetClassPaymentsAmount(long id, CancellationToken ct)
    {
        var payments = await _sportClassRepository.GetClassPaymentsAmount(id, ct);
        return Ok(payments);
    }
}
