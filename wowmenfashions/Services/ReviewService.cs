using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using wowmenfashions.Data;
using wowmenfashions.Data.Entities;
using wowmenfashions.Models;

namespace wowmenfashions.Services;

public class ReviewService : IReviewService
{
    private readonly IReviewRepository _reviewRepository;

    public ReviewService(IReviewRepository reviewRepository)
    {
        _reviewRepository = reviewRepository;
    }

    public async Task<IEnumerable<ReviewDto>> GetReviewsForProductAsync(int productId)
    {
        var reviews = await _reviewRepository.GetByProductIdAsync(productId);
        return reviews.Select(r => new ReviewDto
        {
            Id = r.Id,
            ProductId = r.ProductId,
            CustomerId = r.CustomerId,
            CustomerName = r.Customer?.FirstName + " " + r.Customer?.LastName,
            Rating = r.Rating,
            Comment = r.Comment,
            AdminReply = r.AdminReply,
            CreatedAt = r.CreatedAt,
            UpdatedAt = r.UpdatedAt
        });
    }

    public async Task<ReviewDto?> GetUserReviewForProductAsync(int productId, int customerId)
    {
        var r = await _reviewRepository.GetByUserAndProductAsync(productId, customerId);
        if (r == null) return null;
        
        return new ReviewDto
        {
            Id = r.Id,
            ProductId = r.ProductId,
            CustomerId = r.CustomerId,
            Rating = r.Rating,
            Comment = r.Comment,
            AdminReply = r.AdminReply,
            CreatedAt = r.CreatedAt,
            UpdatedAt = r.UpdatedAt
        };
    }

    public async Task<int> AddReviewAsync(ReviewDto reviewDto)
    {
        var review = new Review
        {
            ProductId = reviewDto.ProductId,
            CustomerId = reviewDto.CustomerId,
            Rating = reviewDto.Rating,
            Comment = reviewDto.Comment
        };
        return await _reviewRepository.CreateAsync(review);
    }

    public async Task UpdateReviewAsync(ReviewDto reviewDto)
    {
        var review = new Review
        {
            Id = reviewDto.Id,
            CustomerId = reviewDto.CustomerId,
            Rating = reviewDto.Rating,
            Comment = reviewDto.Comment
        };
        await _reviewRepository.UpdateAsync(review);
    }

    public async Task DeleteReviewAsync(int reviewId, int customerId)
    {
        await _reviewRepository.DeleteAsync(reviewId, customerId);
    }

    public async Task<IEnumerable<ReviewDto>> GetAllReviewsAdminAsync()
    {
        var reviews = await _reviewRepository.GetAllAsync();
        return reviews.Select(r => new ReviewDto
        {
            Id = r.Id,
            ProductId = r.ProductId,
            ProductName = r.Product?.Name ?? "Unknown Product",
            CustomerId = r.CustomerId,
            CustomerName = r.Customer?.FirstName + " " + r.Customer?.LastName,
            Rating = r.Rating,
            Comment = r.Comment,
            AdminReply = r.AdminReply,
            CreatedAt = r.CreatedAt,
            UpdatedAt = r.UpdatedAt
        });
    }

    public async Task AdminReplyAsync(int reviewId, string reply)
    {
        await _reviewRepository.UpdateAdminReplyAsync(reviewId, reply);
    }

    public async Task AdminDeleteReviewAsync(int reviewId)
    {
        await _reviewRepository.DeleteByAdminAsync(reviewId);
    }
}
