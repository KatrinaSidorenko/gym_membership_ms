using Gymly.Business.Abstractions;
using Gymly.Persistence.Providers;

namespace Gymly.Persistence.Repositories;

public class BaseRepository
{
    protected IDbProvider DbProvider { get; }
    public BaseRepository(IDbProvider dbProvider)
    {
        DbProvider = dbProvider;
    }
}
