using System.Collections.Generic;
using System.Threading.Tasks;
using wowmenfashions.Data.Entities;

namespace wowmenfashions.Data;

public interface IReviewRepository
{
    Task<IEnumerable<Review>> GetByProductIdAsync(int productId);
    Task<Review?> GetByUserAndProductAsync(int productId, int customerId);
    Task<int> CreateAsync(Review review);
    Task UpdateAsync(Review review);
    Task DeleteAsync(int id, int customerId);
    Task<IEnumerable<Review>> GetAllAsync();
    Task UpdateAdminReplyAsync(int id, string adminReply);
    Task DeleteByAdminAsync(int id);
}
