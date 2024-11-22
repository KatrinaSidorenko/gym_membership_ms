using AutoMapper;
using Gymly.Business.Abstractions;
using Gymly.Core.Models;
using Gymly.Shared.Requests.SportClass;
using Gymly.Shared.Responses.SportClass;
using Microsoft.AspNetCore.Mvc;

namespace Gymly.Api.Controllers;

// TODO: add result transformation to some response model
public class SportClassesController : BaseController
{
    private readonly ISportClassRepository _sportClassRepository;
    public SportClassesController(ISportClassRepository sportClassRepository)
    {
        _sportClassRepository = sportClassRepository;
    }

    [HttpGet("active")]
    public async Task<IActionResult> GetAll(CancellationToken ct)
    {
        var date = DateTime.Now;
        var gerResult = await _sportClassRepository.GetAll(ct, date);
        if (!gerResult.IsSuccessful)
        {
            return ServerError(gerResult);
        }

        var mappedClasses = Mapper.Map<IEnumerable<SportClass>, IEnumerable<ExtendedSportClassResponse>>(gerResult.Data);

        return Ok(mappedClasses);
    }

    //todo all active classes for member with IsPaid = true or not

    [HttpPost]
    public async Task<IActionResult> Create([FromBody]CreateSportClassRequest sportClass, CancellationToken ct)
    {
        var mappedClass = Mapper.Map<CreateSportClassRequest, SportClass>(sportClass);
        var createdClassResult = await _sportClassRepository.Create(mappedClass, ct);
        if (!createdClassResult.IsSuccessful)
        {
            return ServerError(createdClassResult);
        }

        return Ok();
    }
}
