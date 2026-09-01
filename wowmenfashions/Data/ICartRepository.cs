using System;
using System.Threading.Tasks;
using wowmenfashions.Data.Entities;

namespace wowmenfashions.Data;

public interface ICartRepository
{
    Task<Cart?> GetCartAsync(Guid? guestCartId, int? customerId);
    Task AddItemAsync(Guid guestCartId, int? customerId, int productId, int quantity, string? selectedColor = null);
    Task UpdateItemQuantityAsync(int cartItemId, int quantity);
    Task RemoveItemAsync(int cartItemId);
}
