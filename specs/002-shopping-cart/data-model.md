# Phase 1: Data Model

## Entities

### `Cart`
Represents a user's shopping session.
- **Id** (UNIQUEIDENTIFIER, Primary Key)
- **CustomerId** (INT, Nullable) - Populated only if authenticated.
- **GuestCartId** (UNIQUEIDENTIFIER, Nullable) - Used for guests via cookie.
- **CreatedAt** (DATETIME2)
- **UpdatedAt** (DATETIME2)

*Constraint*: A cart must have either a `CustomerId` or a `GuestCartId`.

### `CartItem`
Represents an item within the cart.
- **Id** (INT, Primary Key, Identity)
- **CartId** (UNIQUEIDENTIFIER, Foreign Key to Cart.Id)
- **ProductId** (INT, Foreign Key to Product.Id)
- **Quantity** (INT)
- **CreatedAt** (DATETIME2)
- **UpdatedAt** (DATETIME2)

*Constraint*: `Quantity` > 0.
*Constraint*: Unique constraint on `(CartId, ProductId)` so the same product is not added twice (quantity is incremented instead).

## Stored Procedures

1. **`dbo.Cart_Get`**: Takes `@GuestCartId` or `@CustomerId`. Joins `CartItem` with `Product` to return items, current prices, and dynamically calculates totals.
2. **`dbo.Cart_AddItem`**: Takes `@GuestCartId`, `@ProductId`, `@Quantity`. Validates inventory. If product already exists in cart, increments quantity. Creates `Cart` if it doesn't exist.
3. **`dbo.Cart_UpdateItem`**: Takes `@CartItemId`, `@Quantity`. Validates inventory against the new total quantity.
4. **`dbo.Cart_RemoveItem`**: Takes `@CartItemId`. Deletes the row.
