using Microsoft.Data.SqlClient;
using System.Data;

namespace Gymly.Persistence.Providers;

public class DbProvider : IDbProvider
{
    private readonly string _connectionString;
    private readonly IDbConnection _connection;
    public DbProvider(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task<IDbConnection> CreateConnection(CancellationToken ct)
    {
        ct.ThrowIfCancellationRequested();

        var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync(ct);
        return connection;
    }

    public void Dispose()
    {
        if (_connection != null && _connection.State == ConnectionState.Open)
        {
            _connection.Close();
            _connection.Dispose();
        }
    }
}
