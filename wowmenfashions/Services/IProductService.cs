using System.Collections.Generic;
using System.Threading.Tasks;
using wowmenfashions.Models;

namespace wowmenfashions.Services;

public interface IProductService
{
    Task<(IEnumerable<ProductDto> Products, int TotalCount)> GetProductsAsync(string? categorySlug = null, string? color = null, int minPrice = 0, int maxPrice = 10000, int page = 1, int pageSize = 20);
    Task<ProductDto?> GetProductByIdAsync(int id);
    Task<IEnumerable<string>> GetCategoriesAsync();
    Task<IEnumerable<CategoryDto>> GetCategoriesHierarchyAsync();
    Task<IEnumerable<string>> GetColorsAsync();
    
    Task<ProductDto> CreateProductAsync(ProductDto product);
    Task UpdateProductAsync(ProductDto product);
    Task DeleteProductAsync(int id);
}
