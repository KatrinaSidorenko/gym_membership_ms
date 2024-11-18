using Gymly.Core.Models;
using Gymly.Shared.DTOs;
using Gymly.Shared.Requests.SportClass;
using Gymly.Shared.Results;

namespace Gymly.Business.Abstractions;

public interface ISportClassRepository
{
    Task<Result<bool>> Create(SportClass sportClass, CancellationToken ct);
    Task<Result<IEnumerable<ExtendedSportClass>>> GetAll(CancellationToken ct, DateTime? dateTime = null);
}
