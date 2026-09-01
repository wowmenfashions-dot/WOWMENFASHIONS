CREATE PROCEDURE [dbo].[Order_GetByEmail]
    @Email NVARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        Id, 
        OrderDate, 
        Subtotal, 
        ShippingFee, 
        TaxAmount, 
        DiscountAmount, 
        TotalAmount, 
        Status, 
        CustomerName, 
        CustomerEmail, 
        ShippingAddress,
        CourierName,
        TrackingNumber,
        TrackingUrl
    FROM Orders 
    WHERE CustomerEmail = @Email 
    ORDER BY OrderDate DESC;
END
GO
