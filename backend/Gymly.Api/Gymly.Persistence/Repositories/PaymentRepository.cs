using Dapper;
using Gymly.Business.Abstractions;
using Gymly.Core.Models;
using Gymly.Persistence.Providers;
using Gymly.Shared.DTOs;
using Gymly.Shared.Helpers;
using Gymly.Shared.Results;
using Gymly.Shared.Results.Messages;

namespace Gymly.Persistence.Repositories;

// todo: add logical checks exist 
public class PaymentRepository : IPaymentRepository
{
    private readonly IDbProvider _dbProvider;

    public PaymentRepository(IDbProvider dbProvider)
    {
        _dbProvider = dbProvider;
    }

    public async Task<Result<IEnumerable<MemberPayment>>> GetMemberPayments(long memberId, CancellationToken ct)
    {   
        var aliases = new DbAliasesBuilder<MemberPayment>()
            .AddAliases()
            .BuildAliases();
        var function = "fn_member_payments";
        var query = @$"SELECT {aliases} FROM {function}(@memberId)";


        try
        {
            using var connection = await _dbProvider.CreateConnection(ct);
            var payments = await connection.QueryAsync<MemberPayment>(query, new { memberId });

            return Result.Success(payments);
        }
        catch (Exception ex)
        {
            return PaymentStatuses.FailToGetMemberPayments.GetFailureResult<IEnumerable<MemberPayment>>(ex.Message);
        }
    }

    public async Task<Result<long>> Create(Payment payment, CancellationToken ct)
    {
        var query = @"INSERT INTO Payment (enrollment_id, amount_paid, payment_date) 
                      VALUES (@EnrollmentId, @Amount, @PaymentDate);
                      SELECT SCOPE_IDENTITY();";

        try
        {
            using var connection = await _dbProvider.CreateConnection(ct);
            var result = await connection.ExecuteScalarAsync<long>(query, payment);
            return Result.Success(result);
        }
        catch (Exception ex)
        {
            if (ex.Message.Contains(PaymentStatuses.PaymentAmountNotMatchClassPrice.Code))
            {
                return PaymentStatuses.PaymentAmountNotMatchClassPrice.GetFailureResult<long>();
            }

            return PaymentStatuses.FailToCreatePayment.GetFailureResult<long>(ex.Message);
        }
    }
}
