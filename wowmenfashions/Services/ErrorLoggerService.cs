using System;
using System.Threading.Tasks;
using Dapper;
using wowmenfashions.Data;

namespace wowmenfashions.Services;

public class ErrorLoggerService : IErrorLoggerService
{
    private readonly ISqlConnectionFactory _connectionFactory;

    public ErrorLoggerService(ISqlConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task LogErrorAsync(Exception exception, string? userContext = null)
    {
        var sql = @"
            INSERT INTO ErrorLogs (Message, StackTrace, Timestamp, UserContext)
            VALUES (@Message, @StackTrace, @Timestamp, @UserContext);";

        using var connection = _connectionFactory.CreateConnection();
        await connection.ExecuteAsync(sql, new
        {
            Message = exception.Message,
            StackTrace = exception.StackTrace,
            Timestamp = DateTime.UtcNow,
            UserContext = userContext
        });
    }
}
