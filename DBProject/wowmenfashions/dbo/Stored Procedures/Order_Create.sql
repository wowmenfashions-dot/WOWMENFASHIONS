
-- Stored Procedure to Create an Order
CREATE   PROCEDURE dbo.Order_Create
    @Subtotal DECIMAL(18,2),
    @ShippingFee DECIMAL(18,2),
    @TaxAmount DECIMAL(18,2),
    @DiscountAmount DECIMAL(18,2),
    @TotalAmount DECIMAL(18,2),
    @Status NVARCHAR(50),
    @CustomerName NVARCHAR(100),
    @CustomerEmail NVARCHAR(100),
    @ShippingAddressLine1 NVARCHAR(500),
    @ShippingAddressLine2 NVARCHAR(500),
    @ShippingCity NVARCHAR(100),
    @ShippingState NVARCHAR(100),
    @ShippingCountry NVARCHAR(100),
    @ShippingPostalCode NVARCHAR(50),
    @ShippingContactNumber NVARCHAR(50),
    @ShippingLandmark NVARCHAR(255),
    @RazorpayOrderId NVARCHAR(100) = NULL,
    @RazorpayPaymentId NVARCHAR(100) = NULL,
    @PaymentStatus NVARCHAR(50) = 'Pending',
    @OrderId INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO Orders (
        Subtotal, ShippingFee, TaxAmount, DiscountAmount, TotalAmount,
        Status, CustomerName, CustomerEmail, ShippingAddress,
        ShippingAddressLine1, ShippingAddressLine2, ShippingCity,
        ShippingState, ShippingCountry, ShippingPostalCode,
        ShippingContactNumber, ShippingLandmark,
        RazorpayOrderId, RazorpayPaymentId, PaymentStatus, OrderDate
    )
    VALUES (
        @Subtotal, @ShippingFee, @TaxAmount, @DiscountAmount, @TotalAmount,
        @Status, @CustomerName, @CustomerEmail, '',
        @ShippingAddressLine1, @ShippingAddressLine2, @ShippingCity,
        @ShippingState, @ShippingCountry, @ShippingPostalCode,
        @ShippingContactNumber, @ShippingLandmark,
        @RazorpayOrderId, @RazorpayPaymentId, @PaymentStatus, GETDATE()
    );

    SET @OrderId = SCOPE_IDENTITY();
END