using System.Data;
using Dapper;
using wowmenfashions.Data;

namespace wowmenfashions.Services;

public interface IAdminAuditService
{
    Task LogActionAsync(string action, string adminEmail, string? entityId = null);
}

public class AdminAuditService : IAdminAuditService
{
    private readonly ISqlConnectionFactory _connectionFactory;

    public AdminAuditService(ISqlConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task LogActionAsync(string action, string adminEmail, string? entityId = null)
    {
        using var connection = _connectionFactory.CreateConnection();
        var parameters = new DynamicParameters();
        parameters.Add("@Action", action);
        parameters.Add("@AdminEmail", adminEmail);
        parameters.Add("@EntityId", entityId);

        await connection.ExecuteAsync("sp_LogAdminAction", parameters, commandType: CommandType.StoredProcedure);
    }
}
