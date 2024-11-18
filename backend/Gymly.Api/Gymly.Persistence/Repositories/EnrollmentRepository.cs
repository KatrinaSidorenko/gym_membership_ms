using Dapper;
using Gymly.Business.Abstractions;
using Gymly.Core.Models;
using Gymly.Core.Models.Users;
using Gymly.Persistence.Providers;
using Gymly.Shared.Helpers;
using Gymly.Shared.Results;
using Gymly.Shared.Results.Messages;

namespace Gymly.Persistence.Repositories;

public class EnrollmentRepository : IEnrollmentRepository
{
    public IDbProvider DbProvider { get; set; }
    public EnrollmentRepository(IDbProvider dbProvider)
    {
        DbProvider = dbProvider;
    }

    public async Task<Result<IEnumerable<Enrollment>>> GetByClassId(long classId, CancellationToken ct)
    {
        var enrollmentAliases = new DbAliasesBuilder<Enrollment>()
            .AddAliases()
            .AddAlias(e => e.Id, "enrollment_id")
            .BuildAliases("e");

        var memberAliases = new DbAliasesBuilder<Member>()
            .AddAliases()
            .BuildAliases("m");

        var paymentAliases = new DbAliasesBuilder<Payment>()
            .AddAliases()
            .AddAlias(p => p.Id, "payment_id")
            .BuildAliases("p");

        var query = $@"
            SELECT 
                {enrollmentAliases}, 
                {memberAliases}, 
                {paymentAliases}
            FROM Enrollment e
            JOIN Member m ON e.member_id = m.member_id
            LEFT JOIN Payment p ON e.enrollment_id = p.enrollment_id
            WHERE e.class_id = @classId";

        try
        {
            using var connection = await DbProvider.CreateConnection(ct);
            var enrollments = await connection.QueryAsync<Enrollment, Member, Payment, Enrollment>(
                query,
                (enrollment, member, payment) =>
                {
                    enrollment.Member = member;
                    enrollment.Payment = payment;
                    return enrollment;
                },
                new { classId },
                splitOn: "member_id,payment_id");

            return Result.Success(enrollments);
        }
        catch (Exception ex)
        {
            return EnrollmentStatuses.FailToGetEnrollments.GetFailureResult<IEnumerable<Enrollment>>(ex.Message);
        }
    }


    public async Task<Result<bool>> EnrollMemberToClass(Enrollment enrollment, CancellationToken ct)
    {
        var query = @"INSERT INTO Enrollment (class_id, member_id, enrollment_date) 
                      VALUES (@ClassId, @MemberId, @EnrollmentDate);
                      SELECT SCOPE_IDENTITY();";

        try
        {
            using var connection = await DbProvider.CreateConnection(ct);
            var enrollmentId = await connection.ExecuteScalarAsync<long>(query, enrollment);

            return Result.Success(true);
        }
        catch (Exception ex)
        {
            return EnrollmentStatuses.FailToEnrollMemberToClass.GetFailureResult<bool>(ex.Message);
        }
    }
}
