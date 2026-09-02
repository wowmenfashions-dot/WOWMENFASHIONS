using System.Data;
using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
using System.Threading.Tasks;
using wowmenfashions.Models;

namespace wowmenfashions.Repositories;

public class StoreSettingsRepository : IStoreSettingsRepository
{
    private readonly string _connectionString;

    public StoreSettingsRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection") 
            ?? throw new ArgumentNullException(nameof(configuration), "DefaultConnection is missing");
    }

    private IDbConnection CreateConnection() => new SqlConnection(_connectionString);

    public async Task<StoreSettingsDto> GetStoreSettingsAsync()
    {
        using var connection = CreateConnection();
        return await connection.QuerySingleOrDefaultAsync<StoreSettingsDto>(
            "dbo.StoreSettings_Get",
            commandType: CommandType.StoredProcedure) ?? new StoreSettingsDto();
    }

    public async Task<bool> UpdateStoreSettingsAsync(StoreSettingsDto settings)
    {
        using var connection = CreateConnection();
        var parameters = new DynamicParameters();
        parameters.Add("@FreeShippingThreshold", settings.FreeShippingThreshold);
        parameters.Add("@ShippingChargeAmount", settings.ShippingChargeAmount);

        await connection.ExecuteAsync(
            "dbo.StoreSettings_Update",
            parameters,
            commandType: CommandType.StoredProcedure);

        return true;
    }
}
