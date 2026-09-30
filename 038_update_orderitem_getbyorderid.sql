USE wowmenfashions;
GO

CREATE OR ALTER PROCEDURE [dbo].[OrderItem_GetByOrderId]
    @OrderId INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        Id, 
        OrderId, 
        ProductId, 
        ProductName, 
        Price, 
        Quantity, 
        SelectedColor,
        SelectedSize
    FROM OrderItems 
    WHERE OrderId = @OrderId;
END
GO
