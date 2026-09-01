using System;
using System.Threading.Tasks;
using wowmenfashions.Data.Entities;
using Dapper;
using System.Data;

namespace wowmenfashions.Data;

public class CustomerRepository : ICustomerRepository
{
    private readonly ISqlConnectionFactory _connectionFactory;

    public CustomerRepository(ISqlConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<int> CreateCustomerAsync(Customer customer)
    {
        using var connection = _connectionFactory.CreateConnection();
        return await connection.ExecuteScalarAsync<int>(
            "dbo.Customer_Create",
            new
            {
                customer.FirstName,
                customer.LastName,
                customer.Email,
                customer.Phone,
                customer.PasswordHash
            },
            commandType: CommandType.StoredProcedure);
    }

    public async Task<Customer?> GetCustomerByEmailAsync(string email)
    {
        using var connection = _connectionFactory.CreateConnection();
        return await connection.QuerySingleOrDefaultAsync<Customer>(
            "dbo.Customer_GetByEmail",
            new { Email = email },
            commandType: CommandType.StoredProcedure);
    }

    public async Task<Customer?> GetCustomerByIdAsync(int id)
    {
        using var connection = _connectionFactory.CreateConnection();
        return await connection.QuerySingleOrDefaultAsync<Customer>(
            "dbo.Customer_GetById",
            new { Id = id },
            commandType: CommandType.StoredProcedure);
    }

    public async Task UpdateLastLoginAsync(int id)
    {
        using var connection = _connectionFactory.CreateConnection();
        await connection.ExecuteAsync(
            "dbo.Customer_UpdateLastLogin",
            new { Id = id },
            commandType: CommandType.StoredProcedure);
    }

    public async Task UpdateCustomerAsync(Customer customer)
    {
        using var connection = _connectionFactory.CreateConnection();
        await connection.ExecuteAsync(
            "dbo.Customer_Update",
            new
            {
                customer.Id,
                customer.FirstName,
                customer.LastName,
                customer.Phone
            },
            commandType: CommandType.StoredProcedure);
    }
}
