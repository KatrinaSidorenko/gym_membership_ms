﻿using AutoMapper;
using Gymly.Business.Abstractions;
using Gymly.Core.Models;
using Gymly.Infrastructure.Abstractions;
using Gymly.Shared.Requests.Enrollment;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Gymly.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class EnrollmentsController : ControllerBase
{
    private readonly IEnrollmentRepository _enrollmentRepository;
    private readonly IMapper _mapper;
    private readonly IIdentityManager _identityManager;
    public EnrollmentsController(IEnrollmentRepository enrollmentRepository, IMapper mapper, IIdentityManager identityManager)
    {
        _enrollmentRepository = enrollmentRepository;   
        _mapper = mapper;
        _identityManager = identityManager;
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
        var mappedEnrollment = _mapper.Map<CreateEnrollmentRequest, Enrollment>(createEnrollment);
        var identity = _identityManager.GetCurrentUser();
        if (identity == null)
        {
            return Unauthorized();
        }

        mappedEnrollment.MemberId = identity.Id;

        await _enrollmentRepository.EnrollMemberToClass(mappedEnrollment, ct);

        return Ok();
    }
}
