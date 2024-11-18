using Gymly.Core.Models;
using Gymly.Shared.Results;

namespace Gymly.Business.Abstractions;

public interface IEnrollmentRepository
{
    Task<Result<bool>> EnrollMemberToClass(Enrollment enrollment, CancellationToken ct);
    Task<Result<IEnumerable<Enrollment>>> GetByClassId(long classId, CancellationToken ct);
}
