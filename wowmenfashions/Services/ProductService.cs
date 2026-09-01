using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using wowmenfashions.Models;
using wowmenfashions.Data;
using Dapper;

namespace wowmenfashions.Services;

public class ProductService : IProductService
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;
    private readonly IMemoryCache _cache;
    private static readonly TimeSpan CacheDuration = TimeSpan.FromMinutes(15);

    public ProductService(ISqlConnectionFactory sqlConnectionFactory, IMemoryCache cache)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
        _cache = cache;
    }

    private async Task<IEnumerable<CategoryDto>> GetAllCategoriesAsync()
    {
        return await _cache.GetOrCreateAsync("AllCategories", async entry =>
        {
            entry.AbsoluteExpirationRelativeToNow = CacheDuration;
            using var connection = _sqlConnectionFactory.CreateConnection();
            return await connection.QueryAsync<CategoryDto>("Product_GetAllCategories", commandType: System.Data.CommandType.StoredProcedure);
        }) ?? Enumerable.Empty<CategoryDto>();
    }

    private async Task<IEnumerable<ProductDto>> GetAllProductsAsync()
    {
        return await _cache.GetOrCreateAsync("AllProducts", async entry =>
        {
            entry.AbsoluteExpirationRelativeToNow = CacheDuration;
            using var connection = _sqlConnectionFactory.CreateConnection();
            var products = (await connection.QueryAsync<ProductDto>("Product_GetAllProducts", commandType: System.Data.CommandType.StoredProcedure)).ToList();
            
            var imagesQuery = "SELECT ProductId, ImageUrl FROM ProductImages WHERE IsPrimary = 0";
            try 
            {
                var images = await connection.QueryAsync<(int ProductId, string ImageUrl)>(imagesQuery);
                foreach (var p in products)
                {
                    p.AdditionalImages = images.Where(i => i.ProductId == p.Id).Select(i => i.ImageUrl).ToList();
                }
            }
            catch { /* Ignore if table doesn't exist yet */ }

            return products;
        }) ?? Enumerable.Empty<ProductDto>();
    }

    public async Task<(IEnumerable<ProductDto> Products, int TotalCount)> GetProductsAsync(string? categorySlug = null, string? color = null, int minPrice = 0, int maxPrice = 10000, int page = 1, int pageSize = 20)
    {
        var allProducts = await GetAllProductsAsync();
        var allCategories = await GetAllCategoriesAsync();
        var query = allProducts.AsQueryable();

        if (!string.IsNullOrEmpty(categorySlug))
        {
            var category = allCategories.FirstOrDefault(c => c.Slug.Equals(categorySlug, System.StringComparison.OrdinalIgnoreCase));
            if (category != null)
            {
                if (category.ParentId == null)
                {
                    var childIds = allCategories.Where(c => c.ParentId == category.Id).Select(c => c.Id).ToList();
                    query = query.Where(p => p.CategoryId == category.Id || childIds.Contains(p.CategoryId));
                }
                else
                {
                    query = query.Where(p => p.CategoryId == category.Id);
                }
            }
        }

        if (!string.IsNullOrEmpty(color))
            query = query.Where(p => string.Equals(p.Color, color, StringComparison.OrdinalIgnoreCase));

        query = query.Where(p => p.Price >= minPrice && p.Price <= maxPrice);

        int totalCount = query.Count();
        var pagedProducts = query.Skip((page - 1) * pageSize).Take(pageSize).ToList();

        return (pagedProducts, totalCount);
    }

    public async Task<ProductDto?> GetProductByIdAsync(int id)
    {
        var allProducts = await GetAllProductsAsync();
        return allProducts.FirstOrDefault(p => p.Id == id);
    }

    public async Task<IEnumerable<string>> GetCategoriesAsync()
    {
        var allProducts = await GetAllProductsAsync();
        return allProducts.Select(p => p.Category).Distinct();
    }

    public async Task<IEnumerable<CategoryDto>> GetCategoriesHierarchyAsync()
    {
        return await GetAllCategoriesAsync();
    }

    public async Task<IEnumerable<string>> GetColorsAsync()
    {
        var allProducts = await GetAllProductsAsync();
        return allProducts.Where(p => p.Color != null).Select(p => p.Color!).Distinct();
    }

    public async Task<ProductDto> CreateProductAsync(ProductDto product)
    {
        using var connection = _sqlConnectionFactory.CreateConnection();
        var query = @"
            INSERT INTO Products (CategoryId, Category, Name, Description, Price, OriginalPrice, ImageUrl, Color)
            VALUES (@CategoryId, @Category, @Name, @Description, @Price, @OriginalPrice, @ImageUrl, @Color);
            SELECT CAST(SCOPE_IDENTITY() as int);";
            
        product.Id = await connection.QuerySingleAsync<int>(query, product);
        
        if (product.AdditionalImages.Any())
        {
            var imgQuery = "INSERT INTO ProductImages (ProductId, ImageUrl, IsPrimary) VALUES (@ProductId, @ImageUrl, 0)";
            foreach (var img in product.AdditionalImages)
                await connection.ExecuteAsync(imgQuery, new { ProductId = product.Id, ImageUrl = img });
        }
        
        _cache.Remove("AllProducts");
        return product;
    }

    public async Task UpdateProductAsync(ProductDto product)
    {
        using var connection = _sqlConnectionFactory.CreateConnection();
        var query = @"
            UPDATE Products SET 
                CategoryId = @CategoryId, Category = @Category, Name = @Name, 
                Description = @Description, Price = @Price, OriginalPrice = @OriginalPrice, 
                ImageUrl = @ImageUrl, Color = @Color
            WHERE Id = @Id";
        await connection.ExecuteAsync(query, product);

        await connection.ExecuteAsync("DELETE FROM ProductImages WHERE ProductId = @Id", new { Id = product.Id });
        if (product.AdditionalImages.Any())
        {
            var imgQuery = "INSERT INTO ProductImages (ProductId, ImageUrl, IsPrimary) VALUES (@ProductId, @ImageUrl, 0)";
            foreach (var img in product.AdditionalImages)
                await connection.ExecuteAsync(imgQuery, new { ProductId = product.Id, ImageUrl = img });
        }

        _cache.Remove("AllProducts");
    }

    public async Task DeleteProductAsync(int id)
    {
        using var connection = _sqlConnectionFactory.CreateConnection();
        await connection.ExecuteAsync("DELETE FROM Products WHERE Id = @Id", new { Id = id });
        _cache.Remove("AllProducts");
    }
}
