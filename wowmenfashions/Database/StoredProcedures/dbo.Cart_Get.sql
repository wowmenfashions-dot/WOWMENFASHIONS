CREATE OR ALTER PROCEDURE [dbo].[Cart_Get]
    @GuestCartId UNIQUEIDENTIFIER = NULL,
    @CustomerId INT = NULL
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @CartId UNIQUEIDENTIFIER;
    
    -- Find existing cart
    IF @CustomerId IS NOT NULL
        SELECT @CartId = Id FROM Carts WHERE CustomerId = @CustomerId;
    ELSE IF @GuestCartId IS NOT NULL
        SELECT @CartId = Id FROM Carts WHERE GuestCartId = @GuestCartId;

    IF @CartId IS NULL
        RETURN; -- No cart found

    -- Return Cart details
    SELECT Id, CustomerId, GuestCartId, CreatedAt, UpdatedAt
    FROM Carts
    WHERE Id = @CartId;

    -- Return Cart Items joined with actual Product data
    SELECT 
        ci.Id, 
        ci.CartId, 
        ci.ProductId, 
        ci.Quantity, 
        ci.CreatedAt, 
        ci.UpdatedAt,
        p.Name AS ProductName,
        p.Price AS UnitPrice,
        p.ImageUrl AS ProductImageUrl
    FROM CartItems ci
    LEFT JOIN Products p ON ci.ProductId = p.Id
    WHERE ci.CartId = @CartId;

END
GO
