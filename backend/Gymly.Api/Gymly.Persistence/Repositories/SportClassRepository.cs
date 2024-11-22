using Dapper;
using Gymly.Business.Abstractions;
using Gymly.Core.Models;
using Gymly.Persistence.Providers;
using Gymly.Shared.DTOs;
using Gymly.Shared.Helpers;
using Gymly.Shared.Requests.SportClass;
using Gymly.Shared.Results;
using Gymly.Shared.Results.Messages;
using System.Data;
using System.Security.Cryptography;

namespace Gymly.Persistence.Repositories;

public class SportClassRepository : BaseRepository, ISportClassRepository
{
    public SportClassRepository(IDbProvider dbProvider) : base(dbProvider) {}

    public async Task<Result<IEnumerable<ExtendedSportClass>>> GetAll(CancellationToken ct, DateTime? dateTime = null)
    {
        var aliases = new DbAliasesBuilder<ExtendedSportClass>()
            .AddAliases()
            .AddAlias(c => c.Id, "class_id")
            .BuildAliases();
        var query = $"SELECT {aliases}, [dbo].fn_total_sum_of_paid_enrollments(class_id) as PaidEnrollments FROM Class";

        if (dateTime.HasValue)
        {
            query += $" WHERE date >= '{dateTime.Value.ToString("yyyy-MM-dd")}'";
        }

        try
        {
            using var connection = await DbProvider.CreateConnection(ct);
            var classes = await connection.QueryAsync<ExtendedSportClass>(query);

            return Result.Success(classes);
        }
        catch (Exception ex)
        {
            return SportClassStatuses.FailToGetAllClasses.GetFailureResult<IEnumerable<ExtendedSportClass>>(ex.Message);
        }
    }

    public async Task<Result<bool>> Create(SportClass sportClass, CancellationToken ct)
    {
        var procedure = "insert_class";

        try
        {
            using var connection = await DbProvider.CreateConnection(ct);

            var parameters = new DynamicParameters();
            parameters.Add("@class_name", sportClass.Name);
            parameters.Add("@date", sportClass.Date);
            parameters.Add("@instructor_name", sportClass.InstructorName);
            parameters.Add("@price", sportClass.Price);

            var result = await connection.ExecuteScalarAsync<long>(procedure, parameters, commandType: CommandType.StoredProcedure);

            return Result.Success<bool>();
        }
        catch (Exception ex)
        {
            return SportClassStatuses.FailToCreateClass.GetFailureResult<bool>(ex.Message);
        }
       
    }
}
