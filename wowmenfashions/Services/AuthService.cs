using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace wowmenfashions.Services;

public class AuthService : IAuthService
{
    private readonly string _connectionString;

    public AuthService(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection") ?? "";
    }

    public async Task<bool> ValidateAdminAsync(string username, string password)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();

        var query = "SELECT COUNT(1) FROM AdminUsers WHERE Username = @Username AND PasswordHash = @PasswordHash";
        using var command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@Username", username);
        
        // For MVP we use plain text.
        command.Parameters.AddWithValue("@PasswordHash", password); 

        var result = (int)(await command.ExecuteScalarAsync() ?? 0);
        return result > 0;
    }
}
