USE wowmenfashions;
GO

IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[Orders]') AND name = 'RazorpayOrderId')
BEGIN
    ALTER TABLE [dbo].[Orders] ADD [RazorpayOrderId] NVARCHAR(100) NULL;
END
GO

IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[Orders]') AND name = 'PaymentStatus')
BEGIN
    ALTER TABLE [dbo].[Orders] ADD [PaymentStatus] NVARCHAR(50) DEFAULT 'Pending';
END
GO

-- Update Order_Create stored procedure to include these fields
IF OBJECT_ID('dbo.Order_Create', 'P') IS NOT NULL
BEGIN
    EXEC('
    ALTER PROCEDURE [dbo].[Order_Create]
        @Subtotal DECIMAL(18,2),
        @ShippingFee DECIMAL(18,2),
        @TaxAmount DECIMAL(18,2),
        @DiscountAmount DECIMAL(18,2),
        @TotalAmount DECIMAL(18,2),
        @Status NVARCHAR(50),
        @CustomerName NVARCHAR(100),
        @CustomerEmail NVARCHAR(100),
        @ShippingAddress NVARCHAR(MAX),
        @RazorpayOrderId NVARCHAR(100) = NULL,
        @PaymentStatus NVARCHAR(50) = ''Pending'',
        @OrderId INT OUTPUT
    AS
    BEGIN
        INSERT INTO Orders (OrderDate, Subtotal, ShippingFee, TaxAmount, DiscountAmount, TotalAmount, Status, CustomerName, CustomerEmail, ShippingAddress, RazorpayOrderId, PaymentStatus)
        VALUES (GETDATE(), @Subtotal, @ShippingFee, @TaxAmount, @DiscountAmount, @TotalAmount, @Status, @CustomerName, @CustomerEmail, @ShippingAddress, @RazorpayOrderId, @PaymentStatus);

        SET @OrderId = SCOPE_IDENTITY();
    END
    ');
END
GO
