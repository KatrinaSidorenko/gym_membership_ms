using Dapper;
using Gymly.Business.Abstractions;
using Gymly.Core.Models;
using Gymly.Persistence.Providers;
using Gymly.Shared.Helpers;
using Gymly.Shared.Requests.SportClass;
using System.Data;
using System.Security.Cryptography;

namespace Gymly.Persistence.Repositories;

public class SportClassRepository : ISportClassRepository
{
    private readonly IDbProvider _dbProvider;
    public SportClassRepository(IDbProvider dbProvider)
    {
        _dbProvider = dbProvider;
    }



    private const string SelectAllQuery = "SELECT class_id as Id, class_name as Name, date as Date, instructor_name as InstructorName, price as Price FROM Class";

    public async Task<IEnumerable<SportClass>> GetAll(CancellationToken ct)
    {
        var aliases = new DbAliasesBuilder<SportClass>()
            .AddAliases()
            .AddAlias(c => c.Id, "class_id")
            .BuildAliases();
        var query = $"SELECT {aliases} FROM Class";
        using var connection = await _dbProvider.CreateConnection(ct);
        var classes = await connection.QueryAsync<SportClass>(query);

        return classes;
    }

    public async Task<long> Create(SportClass sportClass, CancellationToken ct)
    {
        var procedure = "insert_class";
        using var connection = await _dbProvider.CreateConnection(ct);
        
        var parameters = new DynamicParameters();
        parameters.Add("@class_name", sportClass.Name);
        parameters.Add("@date", sportClass.Date);
        parameters.Add("@instructor_name", sportClass.InstructorName);
        parameters.Add("@price", sportClass.Price);

        var result = await connection.ExecuteScalarAsync<long>(procedure, parameters, commandType: CommandType.StoredProcedure);
        return result;
    }

    public async Task<decimal> GetClassPaymentsAmount(long classId, CancellationToken ct)
    {
        var query = $@"select sum(p.amount_paid) from [dbo].Enrollment e
                inner join [dbo].Payment p
                on e.enrollment_id = p.enrollment_id
                where e.class_id = @classId;";

        using var connection = await _dbProvider.CreateConnection(ct);
        var result = await connection.ExecuteScalarAsync<decimal>(query, new { classId });
        return result;
    }
}
