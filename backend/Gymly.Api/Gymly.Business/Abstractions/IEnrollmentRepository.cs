using Gymly.Core.Models;

namespace Gymly.Business.Abstractions;

public interface IEnrollmentRepository
{
    Task EnrollMemberToClass(Enrollment enrollment, CancellationToken ct);
    Task<IEnumerable<Enrollment>> GetByClassId(long classId, CancellationToken ct);
}
