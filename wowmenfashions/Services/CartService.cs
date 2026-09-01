using System;
using System.Linq;
using System.Threading.Tasks;
using wowmenfashions.Data.Entities;
using wowmenfashions.Data;

namespace wowmenfashions.Services;

public class CartService : ICartService
{
    private readonly ICartRepository _cartRepository;
    private readonly GuestSessionService _guestSessionService;

    public CartService(ICartRepository cartRepository, GuestSessionService guestSessionService)
    {
        _cartRepository = cartRepository;
        _guestSessionService = guestSessionService;
    }

    public async Task<Cart?> GetCartAsync()
    {
        var guestCartId = _guestSessionService.GetOrCreateGuestCartId();
        int? customerId = null; // To be implemented in User Story 4
        return await _cartRepository.GetCartAsync(guestCartId, customerId);
    }

    public async Task AddItemAsync(int productId, int quantity, string? selectedColor = null)
    {
        var guestCartId = _guestSessionService.GetOrCreateGuestCartId();
        int? customerId = null; // To be implemented in User Story 4

        await _cartRepository.AddItemAsync(guestCartId, customerId, productId, quantity, selectedColor);
    }

    public async Task UpdateItemQuantityAsync(int cartItemId, int quantity)
    {
        await _cartRepository.UpdateItemQuantityAsync(cartItemId, quantity);
    }

    public async Task RemoveItemAsync(int cartItemId)
    {
        await _cartRepository.RemoveItemAsync(cartItemId);
    }

    public async Task<int> GetCartItemCountAsync()
    {
        var cart = await GetCartAsync();
        return cart?.Items.Sum(i => i.Quantity) ?? 0;
    }
}
