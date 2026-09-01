CREATE OR ALTER PROCEDURE [dbo].[Cart_Get]
    @GuestCartId UNIQUEIDENTIFIER = NULL,
    @CustomerId INT = NULL
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @CartId UNIQUEIDENTIFIER;

    IF @CustomerId IS NOT NULL
        SELECT @CartId = Id FROM Carts WHERE CustomerId = @CustomerId;
    ELSE IF @GuestCartId IS NOT NULL
        SELECT @CartId = Id FROM Carts WHERE GuestCartId = @GuestCartId;

    IF @CartId IS NOT NULL
    BEGIN
        SELECT Id, CustomerId, GuestCartId, CreatedAt, UpdatedAt
        FROM Carts
        WHERE Id = @CartId;

        SELECT ci.Id, ci.CartId, ci.ProductId, ci.Quantity, ci.SelectedColor, ci.CreatedAt, ci.UpdatedAt, 
               p.Name AS ProductName, p.Price AS UnitPrice, 
               COALESCE(pci.ImageUrl, p.ImageUrl) AS ProductImageUrl
        FROM CartItems ci
        INNER JOIN Products p ON ci.ProductId = p.Id
        LEFT JOIN ProductColorImages pci ON pci.ProductId = p.Id AND pci.ColorName = ci.SelectedColor
        WHERE ci.CartId = @CartId;
    END
END
