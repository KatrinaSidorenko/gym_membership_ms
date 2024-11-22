using Dapper;
using Gymly.Business.Abstractions;
using Gymly.Core.Constants;
using Gymly.Core.Models.Users;
using Gymly.Persistence.Providers;
using Gymly.Shared.Helpers;
using Gymly.Shared.Results;
using Gymly.Shared.Results.Messages;

namespace Gymly.Persistence.Repositories;

public class MemberRepository : BaseRepository, IMemberRepository
{
    public MemberRepository(IDbProvider dbProvider) : base(dbProvider) {}

    public async Task<Result<IEnumerable<Identity>>> GetAll(CancellationToken ct)
    {
        var aliases = new DbAliasesBuilder<Identity>()
            .AddAliases()
            .AddAlias(m => m.Id, "member_id")
            .BuildAliases();
        var query = $"SELECT {aliases} FROM Member";


        try
        {
            using var connection = await DbProvider.CreateConnection(ct);
            var identities = await connection.QueryAsync<Identity>(query);
            identities.ToList().ForEach(i => i.Role = IdentityRole.Member);

            return Result.Success(identities);
        }
        catch (Exception ex)
        {
            return MemberStatuses.FailToGetAllMembers.GetFailureResult<IEnumerable<Identity>>(ex.Message);
        }
    }

    public async Task<Result<long>> Create(Identity identity, CancellationToken ct)
    {
        var query = @"INSERT INTO Member (name, email, phone) 
                      VALUES (@name, @email, @phone);
                      SELECT SCOPE_IDENTITY();";
        try
        {
            using var connection = await DbProvider.CreateConnection(ct);
            var parameters = new DynamicParameters();
            parameters.Add("@name", identity.Name);
            parameters.Add("@email", identity.Email);
            parameters.Add("@phone", identity.Phone);

            var result = await connection.ExecuteScalarAsync<long>(query, parameters);

            return Result.Success(result);
        }
        catch (Exception ex)
        {
            return MemberStatuses.FailToCreateMember.GetFailureResult<long>(ex.Message);
        }
    }
}
