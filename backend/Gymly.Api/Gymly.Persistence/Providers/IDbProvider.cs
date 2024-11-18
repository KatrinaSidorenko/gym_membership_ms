using System.Data;

namespace Gymly.Persistence.Providers;

public interface IDbProvider : IDisposable
{
    Task<IDbConnection> CreateConnection(CancellationToken ct);
}
