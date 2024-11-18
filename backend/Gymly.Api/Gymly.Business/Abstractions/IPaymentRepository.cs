using Gymly.Core.Models;
using Gymly.Shared.DTOs;

namespace Gymly.Business.Abstractions;

public interface IPaymentRepository
{
    Task<long> Create(Payment payment, CancellationToken ct);
    Task<IEnumerable<MemberPayment>> GetMemberPayments(long memberId, CancellationToken ct);
}
