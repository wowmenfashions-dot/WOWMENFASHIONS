
CREATE   PROCEDURE dbo.OrderItem_Create
    @OrderId INT,
    @ProductId INT,
    @ProductName NVARCHAR(255),
    @Price DECIMAL(18,2),
    @Quantity INT,
    @SelectedColor NVARCHAR(50) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO OrderItems (OrderId, ProductId, ProductName, Price, Quantity, SelectedColor)
    VALUES (@OrderId, @ProductId, @ProductName, @Price, @Quantity, @SelectedColor);
END