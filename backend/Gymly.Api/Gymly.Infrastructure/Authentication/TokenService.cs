using Gymly.Core.Models.Users;
using Gymly.Infrastructure.Abstractions;
using Gymly.Shared.Options;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Gymly.Infrastructure.Authentication;

public class TokenService : ITokenService
{
    private readonly JwtOptions _jwtOptions;
    public TokenService(IOptions<JwtOptions> options)
    {
        _jwtOptions = options.Value;
    }
    public string GenerateToken(Identity identity)
    {
        ArgumentNullException.ThrowIfNull(identity);

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Secret));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
            new (ClaimTypes.NameIdentifier, identity.Id.ToString()),
            new (ClaimTypes.Name, identity.Name!),
            new (ClaimTypes.Email, identity.Email!),
            new (ClaimTypes.Role, identity.Role.ToString())
        };

        var expires = DateTime.UtcNow.AddDays(_jwtOptions.ExpiryInMinutes);

        var secToken = new JwtSecurityToken(_jwtOptions.Issuer,
            _jwtOptions.Audience,
            claims,
            expires: expires,
            signingCredentials: credentials);

        var token = new JwtSecurityTokenHandler().WriteToken(secToken);

        return token;
    }
}
