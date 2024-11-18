using Gymly.Core.Constants;
using Gymly.Core.Models.Users;
using Gymly.Infrastructure.Abstractions;
using Gymly.Shared.Extensions;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Gymly.Infrastructure.Authentication;

public class IdentityManager : IIdentityManager
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    public IdentityManager(IHttpContextAccessor httpContextAccessor)
    {

        _httpContextAccessor = httpContextAccessor;

    }
    public Identity GetCurrentUser()
    {
        var claims = _httpContextAccessor?.HttpContext?.User?.Claims;
        if (claims == null || !claims.Any())
        {
            return null;
        }

        var id = claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value.GetLongOrNull();
        var name = claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
        var email = claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
        var role = claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).FirstOrDefault();

        if (id == null || string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(role))
        {
            return null;
        }

        return new Identity
        {
            Id = id.Value,
            Name = name,
            Email = email,
            Role = role
        };

    }
}
