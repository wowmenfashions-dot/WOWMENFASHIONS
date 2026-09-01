CREATE OR ALTER PROCEDURE [dbo].[Order_GetAll]
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
    ORDER BY OrderDate DESC;
END
GO
