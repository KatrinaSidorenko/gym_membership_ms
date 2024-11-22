using Gymly.Core.Models.Users;
using Gymly.Shared.Results;

namespace Gymly.Business.Abstractions;

public interface IIdentityRepository
{
    Task<Result<long>> Create(Identity identity, CancellationToken ct);
    Task<Result<IEnumerable<Identity>>> GetAll(CancellationToken ct);
    Task<Result<Identity>> GetIfExists(string email, string password, CancellationToken ct);
    Task<Result<bool>> IsExists(long memberId, CancellationToken ct);
}
