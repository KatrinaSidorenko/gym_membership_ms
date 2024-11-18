using Gymly.Core.Models;
using Gymly.Shared.DTOs;
using Gymly.Shared.Results;

namespace Gymly.Business.Abstractions;

public interface IPaymentRepository
{
    Task<Result<long>> Create(Payment payment, CancellationToken ct);
    Task<Result<IEnumerable<MemberPayment>>> GetMemberPayments(long memberId, CancellationToken ct);
}
