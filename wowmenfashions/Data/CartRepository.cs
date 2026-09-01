using System;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using wowmenfashions.Data.Entities;

namespace wowmenfashions.Data;

public class CartRepository : ICartRepository
{
    private readonly ISqlConnectionFactory _connectionFactory;

    public CartRepository(ISqlConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<Cart?> GetCartAsync(Guid? guestCartId, int? customerId)
    {
        using var connection = _connectionFactory.CreateConnection();
        var parameters = new DynamicParameters();
        parameters.Add("@GuestCartId", guestCartId == Guid.Empty ? null : guestCartId, DbType.Guid);
        parameters.Add("@CustomerId", customerId, DbType.Int32);

        using var multi = await connection.QueryMultipleAsync("dbo.Cart_Get", parameters, commandType: CommandType.StoredProcedure);
        
        try 
        {
            var cart = await multi.ReadSingleOrDefaultAsync<Cart>();
            if (cart != null)
            {
                var items = await multi.ReadAsync<CartItem>();
                cart.Items = items.AsList();
            }
            return cart;
        }
        catch (ObjectDisposedException) 
        {
            return null; // QueryMultipleAsync fails if no result set is returned by the SP
        }
    }

    public async Task AddItemAsync(Guid guestCartId, int? customerId, int productId, int quantity, string? selectedColor = null)
    {
        using var connection = _connectionFactory.CreateConnection();
        var parameters = new DynamicParameters();
        parameters.Add("@GuestCartId", guestCartId == Guid.Empty ? null : guestCartId, DbType.Guid);
        parameters.Add("@CustomerId", customerId, DbType.Int32);
        parameters.Add("@ProductId", productId, DbType.Int32);
        parameters.Add("@Quantity", quantity, DbType.Int32);
        parameters.Add("@SelectedColor", selectedColor, DbType.String);

        await connection.ExecuteAsync(
            "dbo.Cart_AddItem", 
            parameters, 
            commandType: CommandType.StoredProcedure);
    }

    public async Task UpdateItemQuantityAsync(int cartItemId, int quantity)
    {
        using var connection = _connectionFactory.CreateConnection();
        var parameters = new { CartItemId = cartItemId, Quantity = quantity };
        await connection.ExecuteAsync("dbo.Cart_UpdateItem", parameters, commandType: CommandType.StoredProcedure);
    }

    public async Task RemoveItemAsync(int cartItemId)
    {
        using var connection = _connectionFactory.CreateConnection();
        var parameters = new { CartItemId = cartItemId };
        await connection.ExecuteAsync("dbo.Cart_RemoveItem", parameters, commandType: CommandType.StoredProcedure);
    }
}
