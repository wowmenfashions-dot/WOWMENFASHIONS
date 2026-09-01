CREATE PROCEDURE [dbo].[OrderItem_GetByOrderId]
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
        SelectedColor 
    FROM OrderItems 
    WHERE OrderId = @OrderId;
END
GO
