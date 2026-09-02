using Dapper;
using Microsoft.Data.SqlClient;
using wowmenfashions.Models;

namespace wowmenfashions.Services
{
    public class PageContentService : IPageContentService
    {
        private readonly string _connectionString;

        public PageContentService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection") 
                ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
        }

        public async Task<PageContent?> GetContentByNameAsync(string pageName)
        {
            using var connection = new SqlConnection(_connectionString);
            return await connection.QueryFirstOrDefaultAsync<PageContent>(
                "sp_PageContents_GetByName",
                new { PageName = pageName },
                commandType: System.Data.CommandType.StoredProcedure);
        }

        public async Task UpdateContentAsync(string pageName, string contentHtml)
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.ExecuteAsync(
                "sp_PageContents_Update",
                new { PageName = pageName, ContentHtml = contentHtml },
                commandType: System.Data.CommandType.StoredProcedure);
        }
    }
}
