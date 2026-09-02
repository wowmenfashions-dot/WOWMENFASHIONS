using System;
using System.Threading.Tasks;
using wowmenfashions.Data.Entities;
using wowmenfashions.Data;
using Dapper;
using System.Linq;
using System.Data;

namespace wowmenfashions.Services;

public class CustomerService : ICustomerService
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly ISqlConnectionFactory _sqlConnectionFactory;

    public CustomerService(ICustomerRepository customerRepository, IPasswordHasher passwordHasher, ISqlConnectionFactory sqlConnectionFactory)
    {
        _customerRepository = customerRepository;
        _passwordHasher = passwordHasher;
        _sqlConnectionFactory = sqlConnectionFactory;
    }

    public async Task<(bool Success, string ErrorMessage)> RegisterAsync(string firstName, string lastName, string email, string password, string? phone)
    {
        try
        {
            var existingCustomer = await _customerRepository.GetCustomerByEmailAsync(email);
            if (existingCustomer != null)
            {
                return (false, "Email is already registered.");
            }

            var passwordHash = _passwordHasher.HashPassword(password);
            var customer = new Customer
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                Phone = phone,
                PasswordHash = passwordHash
            };

            await _customerRepository.CreateCustomerAsync(customer);
            return (true, string.Empty);
        }
        catch (Exception)
        {
            return (false, "An error occurred during registration. Please try again.");
        }
    }

    public async Task<(bool Success, Customer? Customer, string ErrorMessage)> LoginAsync(string email, string password)
    {
        try
        {
            var customer = await _customerRepository.GetCustomerByEmailAsync(email);
            if (customer == null || !customer.IsActive)
            {
                return (false, null, "Invalid email or password.");
            }

            if (!_passwordHasher.VerifyPassword(password, customer.PasswordHash))
            {
                return (false, null, "Invalid email or password.");
            }

            await _customerRepository.UpdateLastLoginAsync(customer.Id);
            
            return (true, customer, string.Empty);
        }
        catch (Exception)
        {
            return (false, null, "An error occurred during login. Please try again.");
        }
    }

    public async Task<Customer?> GetCustomerProfileAsync(int id)
    {
        return await _customerRepository.GetCustomerByIdAsync(id);
    }

    public async Task<Customer?> GetCustomerByEmailAsync(string email)
    {
        return await _customerRepository.GetCustomerByEmailAsync(email);
    }

    public async Task<(bool Success, string ErrorMessage)> UpdateProfileAsync(int id, string firstName, string lastName, string? phone)
    {
        try
        {
            var customer = await _customerRepository.GetCustomerByIdAsync(id);
            if (customer == null)
            {
                return (false, "Customer not found.");
            }

            customer.FirstName = firstName;
            customer.LastName = lastName;
            customer.Phone = phone;

            await _customerRepository.UpdateCustomerAsync(customer);
            return (true, string.Empty);
        }
        catch (Exception)
        {
            return (false, "An error occurred while updating the profile.");
        }
    }

    public async Task<string> GeneratePasswordResetTokenAsync(string email)
    {
        // Mock implementation
        await Task.Delay(500); // Simulate network latency
        return "mock-token-123"; 
    }

    public async Task<System.Collections.Generic.IEnumerable<wowmenfashions.Models.OrderDto>> GetOrdersByEmailAsync(string email)
    {
        using var connection = _sqlConnectionFactory.CreateConnection();
        var orders = (await connection.QueryAsync<wowmenfashions.Models.OrderDto>(
            "dbo.Order_GetByEmail",
            new { Email = email },
            commandType: CommandType.StoredProcedure)).AsList();

        foreach(var order in orders)
        {
            var items = await connection.QueryAsync<wowmenfashions.Models.OrderItemDto>(
                "dbo.OrderItem_GetByOrderId",
                new { OrderId = order.Id },
                commandType: CommandType.StoredProcedure);
            order.Items = items.AsList();
        }

        return orders;
    }

    public async Task<System.Collections.Generic.IEnumerable<wowmenfashions.Models.CustomerAddressDto>> GetAddressesAsync(int customerId)
    {
        using var connection = _sqlConnectionFactory.CreateConnection();
        return await connection.QueryAsync<wowmenfashions.Models.CustomerAddressDto>(
            "SELECT Id, CustomerId, FullName, AddressLine, AddressLine2, City, State, PostalCode, Country, ContactNumber, Landmark, IsDefaultShipping, IsDefaultBilling FROM CustomerAddresses WHERE CustomerId = @CustomerId",
            new { CustomerId = customerId });
    }

    public async Task<bool> SaveAddressAsync(wowmenfashions.Models.CustomerAddressDto address)
    {
        using var connection = _sqlConnectionFactory.CreateConnection();
        
        if (address.IsDefaultShipping)
            await connection.ExecuteAsync("UPDATE CustomerAddresses SET IsDefaultShipping = 0 WHERE CustomerId = @CustomerId", new { address.CustomerId });
            
        if (address.IsDefaultBilling)
            await connection.ExecuteAsync("UPDATE CustomerAddresses SET IsDefaultBilling = 0 WHERE CustomerId = @CustomerId", new { address.CustomerId });

        if (address.Id == 0)
        {
            await connection.ExecuteAsync(
                "dbo.Address_Create",
                address,
                commandType: CommandType.StoredProcedure);
        }
        else
        {
            await connection.ExecuteAsync(
                "dbo.Address_Update",
                address,
                commandType: CommandType.StoredProcedure);
        }
        return true;
    }

    public async Task<bool> DeleteAddressAsync(int addressId)
    {
        using var connection = _sqlConnectionFactory.CreateConnection();
        await connection.ExecuteAsync("DELETE FROM CustomerAddresses WHERE Id = @Id", new { Id = addressId });
        return true;
    }
}
