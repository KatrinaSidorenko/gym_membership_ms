using Dapper;
using Gymly.Business.Abstractions;
using Gymly.Core.Models;
using Gymly.Persistence.Providers;
using Gymly.Shared.DTOs;
using Gymly.Shared.Helpers;

namespace Gymly.Persistence.Repositories;

// todo: add logical checks exist 
public class PaymentRepository : IPaymentRepository
{
    private readonly IDbProvider _dbProvider;

    public PaymentRepository(IDbProvider dbProvider)
    {
        _dbProvider = dbProvider;
    }

    public async Task<IEnumerable<MemberPayment>> GetMemberPayments(long memberId, CancellationToken ct)
    {   
        var aliases = new DbAliasesBuilder<MemberPayment>()
            .AddAliases()
            .BuildAliases();
        var function = "fn_member_payments";
        var query = @$"SELECT {aliases} FROM {function}(@memberId)";
        using var connection = await _dbProvider.CreateConnection(ct);
        var payments = await connection.QueryAsync<MemberPayment>(query, new { memberId });

        return payments;
    }

    public async Task<long> Create(Payment payment, CancellationToken ct)
    {
        var query = @"INSERT INTO Payment (enrollment_id, amount_paid, payment_date) 
                      VALUES (@EnrollmentId, @Amount, @PaymentDate);
                      SELECT SCOPE_IDENTITY();";
        using var connection = await _dbProvider.CreateConnection(ct);
        var result = await connection.ExecuteScalarAsync<long>(query, payment);
        return result;
    }
}
