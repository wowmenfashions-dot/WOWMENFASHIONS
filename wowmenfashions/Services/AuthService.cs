using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace wowmenfashions.Services;

public class AuthService : IAuthService
{
    private readonly string _connectionString;
    private readonly IPasswordHasher _passwordHasher;

    public AuthService(IConfiguration configuration, IPasswordHasher passwordHasher)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection") ?? "";
        _passwordHasher = passwordHasher;
    }

    public async Task<bool> ValidateAdminAsync(string username, string password)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();

        var query = "SELECT PasswordHash FROM AdminUsers WHERE Username = @Username";
        using var command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@Username", username);
        
        var storedHash = await command.ExecuteScalarAsync() as string;
        
        if (string.IsNullOrEmpty(storedHash))
        {
            return false;
        }

        // Fallback for MVP plain-text passwords
        if (storedHash == password)
        {
            return true;
        }

        return _passwordHasher.VerifyPassword(password, storedHash);
    }
}
