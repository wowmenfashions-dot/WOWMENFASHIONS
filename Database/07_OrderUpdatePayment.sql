USE wowmenfashions;
GO

IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[Orders]') AND name = 'RazorpaySignature')
BEGIN
    ALTER TABLE [dbo].[Orders] ADD [RazorpaySignature] NVARCHAR(200) NULL;
END
GO

CREATE OR ALTER PROCEDURE dbo.Order_UpdatePayment
    @OrderId INT,
    @RazorpayOrderId NVARCHAR(100),
    @RazorpayPaymentId NVARCHAR(100),
    @RazorpaySignature NVARCHAR(200),
    @PaymentStatus NVARCHAR(50),
    @Status NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE Orders
    SET 
        RazorpayOrderId = @RazorpayOrderId,
        RazorpayPaymentId = @RazorpayPaymentId,
        RazorpaySignature = @RazorpaySignature,
        PaymentStatus = @PaymentStatus,
        Status = @Status
    WHERE Id = @OrderId;
END
GO
