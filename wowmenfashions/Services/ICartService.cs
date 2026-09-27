using System;
using System.Threading.Tasks;
using wowmenfashions.Data.Entities;

namespace wowmenfashions.Services;

public interface ICartService
{
    Task<Cart?> GetCartAsync();
    Task AddItemAsync(int productId, int quantity, string? selectedColor = null, string? selectedSize = null);
    Task UpdateItemQuantityAsync(int cartItemId, int quantity);
    Task RemoveItemAsync(int cartItemId);
    Task<int> GetCartItemCountAsync();
    event Action? OnCartChanged;
}
