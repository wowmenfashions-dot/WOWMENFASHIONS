using System.Threading.Tasks;
using wowmenfashions.Data.Entities;

namespace wowmenfashions.Data;

public interface ICustomerRepository
{
    Task<int> CreateCustomerAsync(Customer customer);
    Task<Customer?> GetCustomerByEmailAsync(string email);
    Task<Customer?> GetCustomerByIdAsync(int id);
    Task UpdateLastLoginAsync(int id);
    Task UpdateCustomerAsync(Customer customer);
}
