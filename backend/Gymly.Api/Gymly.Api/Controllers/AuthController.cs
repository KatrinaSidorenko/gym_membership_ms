using AutoMapper;
using Gymly.Business.Abstractions;
using Gymly.Core.Models.Users;
using Gymly.Infrastructure.Abstractions;
using Gymly.Shared.Requests.Login;
using Gymly.Shared.Responses.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Gymly.Api.Controllers;

public class AuthController : BaseController
{
    private readonly ITokenService _tokenService;
    private readonly IIdentityRepository _identityRepository;
    public AuthController(ITokenService tokenService, IIdentityRepository identityRepository)
    {
        _tokenService = tokenService;
        _identityRepository = identityRepository;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest userInfo, CancellationToken ct)
    {
        var identityResult = await _identityRepository.GetIfExists(userInfo.Email, userInfo.Password, ct);
        if (identityResult == null)
        {
            return ServerError(identityResult);
        }

        var token = _tokenService.GenerateToken(identityResult.Data);

        return Ok(new LoginResponse() { Token = token });
    }

    [HttpGet("account")]
    public async Task<IActionResult> GetIdentity(CancellationToken ct)
    {
        var identity = IdentityManager.GetCurrentUser();
        if (identity == null)
        {
            return Unauthorized();
        }

        return Ok(Mapper.Map<UserResponse>(identity));
    }

}
