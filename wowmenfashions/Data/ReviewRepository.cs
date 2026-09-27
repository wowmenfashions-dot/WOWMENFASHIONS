using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using wowmenfashions.Data.Entities;

namespace wowmenfashions.Data;

public class ReviewRepository : IReviewRepository
{
    private readonly ISqlConnectionFactory _connectionFactory;

    public ReviewRepository(ISqlConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<IEnumerable<Review>> GetByProductIdAsync(int productId)
    {
        using var connection = _connectionFactory.CreateConnection();
        var sql = @"
            SELECT r.*, c.Id, c.FirstName, c.LastName 
            FROM Reviews r
            INNER JOIN Customers c ON r.CustomerId = c.Id
            WHERE r.ProductId = @ProductId
            ORDER BY r.CreatedAt DESC";
        
        return await connection.QueryAsync<Review, Customer, Review>(
            sql, 
            (review, customer) => 
            {
                review.Customer = customer;
                return review;
            },
            new { ProductId = productId });
    }

    public async Task<Review?> GetByUserAndProductAsync(int productId, int customerId)
    {
        using var connection = _connectionFactory.CreateConnection();
        var sql = "SELECT * FROM Reviews WHERE ProductId = @ProductId AND CustomerId = @CustomerId";
        return await connection.QuerySingleOrDefaultAsync<Review>(sql, new { ProductId = productId, CustomerId = customerId });
    }

    public async Task<int> CreateAsync(Review review)
    {
        using var connection = _connectionFactory.CreateConnection();
        var sql = @"
            INSERT INTO Reviews (ProductId, CustomerId, Rating, Comment, CreatedAt)
            VALUES (@ProductId, @CustomerId, @Rating, @Comment, GETUTCDATE());
            SELECT CAST(SCOPE_IDENTITY() as int);";
            
        return await connection.ExecuteScalarAsync<int>(sql, new { 
            review.ProductId, 
            review.CustomerId, 
            review.Rating, 
            review.Comment 
        });
    }

    public async Task UpdateAsync(Review review)
    {
        using var connection = _connectionFactory.CreateConnection();
        var sql = @"
            UPDATE Reviews 
            SET Rating = @Rating, Comment = @Comment, UpdatedAt = GETUTCDATE()
            WHERE Id = @Id AND CustomerId = @CustomerId";
            
        await connection.ExecuteAsync(sql, new { 
            review.Id, 
            review.CustomerId, 
            review.Rating, 
            review.Comment 
        });
    }

    public async Task DeleteAsync(int id, int customerId)
    {
        using var connection = _connectionFactory.CreateConnection();
        var sql = "DELETE FROM Reviews WHERE Id = @Id AND CustomerId = @CustomerId";
        await connection.ExecuteAsync(sql, new { Id = id, CustomerId = customerId });
    }

    public async Task<IEnumerable<Review>> GetAllAsync()
    {
        using var connection = _connectionFactory.CreateConnection();
        var sql = @"
            SELECT r.*, c.Id, c.FirstName, c.LastName, p.Id, p.Name
            FROM Reviews r
            INNER JOIN Customers c ON r.CustomerId = c.Id
            INNER JOIN Products p ON r.ProductId = p.Id
            ORDER BY r.CreatedAt DESC";
            
        return await connection.QueryAsync<Review, Customer, wowmenfashions.Models.ProductDto, Review>(
            sql, 
            (review, customer, product) => 
            {
                review.Customer = customer;
                review.Product = product;
                return review;
            });
    }

    public async Task UpdateAdminReplyAsync(int id, string adminReply)
    {
        using var connection = _connectionFactory.CreateConnection();
        var sql = "UPDATE Reviews SET AdminReply = @AdminReply WHERE Id = @Id";
        await connection.ExecuteAsync(sql, new { Id = id, AdminReply = adminReply });
    }

    public async Task DeleteByAdminAsync(int id)
    {
        using var connection = _connectionFactory.CreateConnection();
        var sql = "DELETE FROM Reviews WHERE Id = @Id";
        await connection.ExecuteAsync(sql, new { Id = id });
    }
}
