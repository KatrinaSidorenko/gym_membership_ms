using Gymly.Core.Models.Users;

namespace Gymly.Infrastructure.Abstractions;

public interface ITokenService
{
    string GenerateToken(Identity identity);
}
