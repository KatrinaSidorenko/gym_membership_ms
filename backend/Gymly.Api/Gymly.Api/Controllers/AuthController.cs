using Gymly.Core.Models.Users;
using Gymly.Infrastructure.Abstractions;
using Gymly.Shared.Requests.Login;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Gymly.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly ITokenService _tokenService;
    public AuthController(ITokenService tokenService)
    {
        _tokenService = tokenService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest userInfo, CancellationToken ct)
    {
        var identity = new Identity
        {
            Id = 1,
            Email = userInfo.Email,
            Name = "Kate",
            Role = "Admin"
        };

        var token = _tokenService.GenerateToken(identity);

        return Ok(token);
    }

}
