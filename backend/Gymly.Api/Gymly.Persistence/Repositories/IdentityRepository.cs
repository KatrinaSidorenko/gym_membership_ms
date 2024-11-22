using Dapper;
using Gymly.Business.Abstractions;
using Gymly.Core.Constants;
using Gymly.Core.Models.Users;
using Gymly.Persistence.Providers;
using Gymly.Shared.Helpers;
using Gymly.Shared.Results;
using Gymly.Shared.Results.Messages;

namespace Gymly.Persistence.Repositories;

public class IdentityRepository : BaseRepository, IIdentityRepository
{
    public IdentityRepository(IDbProvider dbProvider) : base(dbProvider) {}

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
        var query = @"INSERT INTO Member (name, email, phone_number, password) 
                      VALUES (@name, @email, @phone, @password);
                      SELECT SCOPE_IDENTITY();";
        try
        {
            using var connection = await DbProvider.CreateConnection(ct);
            var parameters = new DynamicParameters();
            parameters.Add("@name", identity.Name);
            parameters.Add("@email", identity.Email);
            parameters.Add("@phone", identity.Phone);

            var hashedPassword = new PasswordHasher().HashPassword(identity.Password);
            parameters.Add("@password", hashedPassword);

            var result = await connection.ExecuteScalarAsync<long>(query, parameters);

            return Result.Success(result);
        }
        catch (Exception ex)
        {
            return MemberStatuses.FailToCreateMember.GetFailureResult<long>(ex.Message);
        }
    }

    public async Task<Result<bool>> IsExists(long memberId, CancellationToken ct)
    {
        var query = "SELECT COUNT(*) FROM Member WHERE member_id = @memberId";

        try
        {
            using var connection = await DbProvider.CreateConnection(ct);
            var count = await connection.ExecuteScalarAsync<int>(query, new { memberId });

            return Result.Success(count > 0);
        }
        catch (Exception ex)
        {
            return MemberStatuses.FailToCheckMemberExistence.GetFailureResult<bool>(ex.Message);
        }
    }

    public async Task<Result<Identity>> GetIfExists(string email, string password, CancellationToken ct)
    {
        var aliases = new DbAliasesBuilder<Identity>()
            .AddAliases()
            .AddAlias(m => m.GetId(), "member_id")
            .BuildAliases();

        var query = $"SELECT {aliases} FROM Member WHERE email = @email AND password = @password";

        try
        {
            var hasher = new PasswordHasher();
            password = hasher.HashPassword(password);

            using var connection = await DbProvider.CreateConnection(ct);
            var member = await connection.QueryFirstOrDefaultAsync<Identity>(query, new { email, password });
            if (member == null)
            {
                return MemberStatuses.MemberNotFound.GetFailureResult<Identity>();
            }

            return Result.Success(member);
        }
        catch (Exception ex)
        {
            return MemberStatuses.FailToGetMember.GetFailureResult<Identity>(ex.Message);
        }
    }
}
