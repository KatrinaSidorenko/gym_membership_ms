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
        var enrollments = await _enrollmentRepository.GetByClassId(classId, ct);
        if (!enrollments.IsSuccessful)
        {
            return BadRequest(enrollments.Code);
        }

        return Ok(enrollments.Data);
    }

    [HttpPost]
    public async Task<IActionResult> EnrollMemberToClass([FromBody] CreateEnrollmentRequest createEnrollment, CancellationToken ct)
    {
        var mappedEnrollment = Mapper.Map<CreateEnrollmentRequest, Enrollment>(createEnrollment);
        if (CurrentUser is null) { return Unauthorized(); }

        mappedEnrollment.MemberId = CurrentUser.Id;

        await _enrollmentRepository.EnrollMemberToClass(mappedEnrollment, ct);

        return Ok();
    }
}
