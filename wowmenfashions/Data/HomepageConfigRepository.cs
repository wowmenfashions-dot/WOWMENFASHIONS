using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using wowmenfashions.Models;

namespace wowmenfashions.Data;

public interface IHomepageConfigRepository
{
    Task<HomepageConfig?> GetConfigAsync(string configKey);
    Task SetConfigAsync(string configKey, string configValue);
    Task<IEnumerable<HomepageConfig>> GetAllConfigsAsync();
}

public class HomepageConfigRepository : IHomepageConfigRepository
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;

    public HomepageConfigRepository(ISqlConnectionFactory sqlConnectionFactory)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
    }

    public async Task<HomepageConfig?> GetConfigAsync(string configKey)
    {
        using var connection = _sqlConnectionFactory.CreateConnection();
        var query = "SELECT * FROM HomepageConfig WHERE ConfigKey = @ConfigKey";
        return await connection.QuerySingleOrDefaultAsync<HomepageConfig>(query, new { ConfigKey = configKey });
    }

    public async Task SetConfigAsync(string configKey, string configValue)
    {
        using var connection = _sqlConnectionFactory.CreateConnection();
        var query = @"
            IF EXISTS (SELECT 1 FROM HomepageConfig WHERE ConfigKey = @ConfigKey)
            BEGIN
                UPDATE HomepageConfig SET ConfigValue = @ConfigValue, UpdatedAt = SYSUTCDATETIME() WHERE ConfigKey = @ConfigKey;
            END
            ELSE
            BEGIN
                INSERT INTO HomepageConfig (ConfigKey, ConfigValue) VALUES (@ConfigKey, @ConfigValue);
            END";
        await connection.ExecuteAsync(query, new { ConfigKey = configKey, ConfigValue = configValue });
    }

    public async Task<IEnumerable<HomepageConfig>> GetAllConfigsAsync()
    {
        using var connection = _sqlConnectionFactory.CreateConnection();
        var query = "SELECT * FROM HomepageConfig";
        return await connection.QueryAsync<HomepageConfig>(query);
    }
}
