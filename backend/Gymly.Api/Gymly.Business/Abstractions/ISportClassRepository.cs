using Gymly.Core.Models;
using Gymly.Shared.Requests.SportClass;

namespace Gymly.Business.Abstractions;

public interface ISportClassRepository
{
    Task<long> Create(SportClass sportClass, CancellationToken ct);
    Task<IEnumerable<SportClass>> GetAll(CancellationToken ct);
    Task<decimal> GetClassPaymentsAmount(long classId, CancellationToken ct);
}
