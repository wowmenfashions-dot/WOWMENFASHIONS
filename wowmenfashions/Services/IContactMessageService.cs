using System.Collections.Generic;
using System.Threading.Tasks;
using wowmenfashions.Models;

namespace wowmenfashions.Services
{
    public interface IContactMessageService
    {
        Task<int> CreateMessageAsync(ContactMessage message);
        Task<IEnumerable<ContactMessage>> GetAllMessagesAsync();
        Task MarkAsReadAsync(int id);
    }
}
