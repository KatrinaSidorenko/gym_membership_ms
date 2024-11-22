using AutoMapper;
using Gymly.Business.Abstractions;
using Gymly.Core.Models.Users;
using Gymly.Shared.Requests.Member;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Gymly.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MembersController : BaseController
{
    private readonly IMemberRepository _memberRepository;
    public MembersController(IMemberRepository memberRepository) 
    {
        _memberRepository = memberRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken ct)
    {
        var members = await _memberRepository.GetAll(ct);
        if (!members.IsSuccessful)
        {
            return BadRequest(members.Code);
        }

        return Ok(members.Data);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateMemberRequest member, CancellationToken ct)
    {
        var mappedIdentity = Mapper.Map<CreateMemberRequest, Identity>(member);
        var createdMember = await _memberRepository.Create(mappedIdentity, ct);
        if (!createdMember.IsSuccessful)
        {
            return BadRequest(createdMember.Code);
        }

        return Ok();
    }
}
