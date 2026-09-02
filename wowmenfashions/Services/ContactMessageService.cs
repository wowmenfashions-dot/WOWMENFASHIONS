using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using wowmenfashions.Models;

namespace wowmenfashions.Services
{
    public class ContactMessageService : IContactMessageService
    {
        private readonly string _connectionString;

        public ContactMessageService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection") 
                ?? throw new System.ArgumentNullException(nameof(configuration), "DefaultConnection is missing in appsettings.json");
        }

        public async Task<int> CreateMessageAsync(ContactMessage message)
        {
            using var connection = new SqlConnection(_connectionString);
            var parameters = new
            {
                message.Name,
                message.Email,
                message.Subject,
                message.Message
            };

            return await connection.QuerySingleAsync<int>(
                "sp_ContactMessages_Insert",
                parameters,
                commandType: CommandType.StoredProcedure
            );
        }

        public async Task<IEnumerable<ContactMessage>> GetAllMessagesAsync()
        {
            using var connection = new SqlConnection(_connectionString);
            return await connection.QueryAsync<ContactMessage>(
                "sp_ContactMessages_GetAll",
                commandType: CommandType.StoredProcedure
            );
        }

        public async Task MarkAsReadAsync(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.ExecuteAsync(
                "sp_ContactMessages_MarkAsRead",
                new { Id = id },
                commandType: CommandType.StoredProcedure
            );
        }
    }
}
