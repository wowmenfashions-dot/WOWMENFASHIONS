using System.Threading.Tasks;
using wowmenfashions.Data.Entities;

namespace wowmenfashions.Services;

public interface ICustomerService
{
    Task<(bool Success, string ErrorMessage)> RegisterAsync(string firstName, string lastName, string email, string password, string? phone);
    Task<(bool Success, Customer? Customer, string ErrorMessage)> LoginAsync(string email, string password);
    Task<Customer?> GetCustomerProfileAsync(int id);
    Task<Customer?> GetCustomerByEmailAsync(string email);
    Task<(bool Success, string ErrorMessage)> UpdateProfileAsync(int id, string firstName, string lastName, string? phone);
    Task<string> GeneratePasswordResetTokenAsync(string email); // Mock for now
    Task<System.Collections.Generic.IEnumerable<wowmenfashions.Models.OrderDto>> GetOrdersByEmailAsync(string email);
    Task<System.Collections.Generic.IEnumerable<wowmenfashions.Models.CustomerAddressDto>> GetAddressesAsync(int customerId);
    Task<bool> SaveAddressAsync(wowmenfashions.Models.CustomerAddressDto address);
    Task<bool> DeleteAddressAsync(int addressId);
}
