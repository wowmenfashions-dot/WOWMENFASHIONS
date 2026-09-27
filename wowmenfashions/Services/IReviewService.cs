using System.Collections.Generic;
using System.Threading.Tasks;
using wowmenfashions.Models;

namespace wowmenfashions.Services;

public interface IReviewService
{
    Task<IEnumerable<ReviewDto>> GetReviewsForProductAsync(int productId);
    Task<ReviewDto?> GetUserReviewForProductAsync(int productId, int customerId);
    Task<int> AddReviewAsync(ReviewDto reviewDto);
    Task UpdateReviewAsync(ReviewDto reviewDto);
    Task DeleteReviewAsync(int reviewId, int customerId);
    Task<IEnumerable<ReviewDto>> GetAllReviewsAdminAsync();
    Task AdminReplyAsync(int reviewId, string reply);
    Task AdminDeleteReviewAsync(int reviewId);
}
