CREATE   PROCEDURE [dbo].[Order_GetAll]
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
        ShippingAddressLine1,
        ShippingAddressLine2,
        ShippingCity,
        ShippingState,
        ShippingCountry,
        ShippingPostalCode,
        ShippingContactNumber,
        ShippingLandmark,
        CourierName,
        TrackingNumber,
        TrackingUrl,
        PaymentStatus,
        RazorpayOrderId,
        RazorpayPaymentId
    FROM Orders 
    ORDER BY OrderDate DESC;
END