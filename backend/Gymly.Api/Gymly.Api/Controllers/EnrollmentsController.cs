using AutoMapper;
using Gymly.Business.Abstractions;
using Gymly.Core.Models;
using Gymly.Infrastructure.Abstractions;
using Gymly.Shared.Requests.Enrollment;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Gymly.Api.Controllers;


[Authorize]
public class EnrollmentsController : BaseController
{
    private readonly IEnrollmentRepository _enrollmentRepository;
    public EnrollmentsController(IEnrollmentRepository enrollmentRepository)
    {
        _enrollmentRepository = enrollmentRepository;   
    }

    [HttpGet("{classId}")]
    public async Task<IActionResult> GetByClassId(int classId, CancellationToken ct)
    {
        var enrollmentsResult = await _enrollmentRepository.GetByClassId(classId, ct);
        if (!enrollmentsResult.IsSuccessful)
        {
            return ServerError(enrollmentsResult);
        }

        return Ok(enrollmentsResult.Data);
    }

    [HttpPost]
    public async Task<IActionResult> EnrollMemberToClass([FromBody] CreateEnrollmentRequest createEnrollment, CancellationToken ct)
    {
        var mappedEnrollment = Mapper.Map<CreateEnrollmentRequest, Enrollment>(createEnrollment);
        if (CurrentUser is null) { return Unauthorized(); }

        mappedEnrollment.MemberId = CurrentUser.Id;
        var enrollmentResult = await _enrollmentRepository.EnrollMemberToClass(mappedEnrollment, ct);
        // todo: create mapping for EnrollmentResponse
        if (!enrollmentResult.IsSuccessful)
        {
            return ServerError(enrollmentResult);
        }

        return Ok();
    }
}
