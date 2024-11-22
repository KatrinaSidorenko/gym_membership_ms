using AutoMapper;
using Gymly.Core.Models.Users;
using Gymly.Infrastructure.Abstractions;
using Gymly.Shared.Requests.Login;
using Gymly.Shared.Responses.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Gymly.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly ITokenService _tokenService;
    private readonly IIdentityManager _identityManager;
    private readonly IMapper _mapper;
    public AuthController(ITokenService tokenService, IIdentityManager identityManager, IMapper mapper)
    {
        _tokenService = tokenService;
        _identityManager = identityManager;
        _mapper = mapper;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest userInfo, CancellationToken ct)
    {
        var identity = new Identity
        {
            Id = 1,
            Email = userInfo.Email,
            Name = "Kate",
            Role = "Admin",
            Phone = "123456789"
        };

        var token = _tokenService.GenerateToken(identity);

        return Ok(new LoginResponse() { Token = token });
    }

    [HttpGet("account")]
    public async Task<IActionResult> GetIdentity(CancellationToken ct)
    {
        var identity = _identityManager.GetCurrentUser();
        if (identity == null)
        {
            return Unauthorized();
        }

        return Ok(_mapper.Map<UserResponse>(identity));
    }

}
