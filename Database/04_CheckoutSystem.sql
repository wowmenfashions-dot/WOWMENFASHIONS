USE [WOWMENFASHIONS];
GO

-- Create Orders Table
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Orders')
BEGIN
    CREATE TABLE Orders (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        OrderDate DATETIME NOT NULL DEFAULT GETDATE(),
        Subtotal DECIMAL(18,2) NOT NULL,
        ShippingFee DECIMAL(18,2) NOT NULL,
        TaxAmount DECIMAL(18,2) NOT NULL,
        DiscountAmount DECIMAL(18,2) NOT NULL,
        TotalAmount DECIMAL(18,2) NOT NULL,
        Status NVARCHAR(50) NOT NULL,
        CustomerName NVARCHAR(100) NOT NULL,
        CustomerEmail NVARCHAR(100) NOT NULL,
        ShippingAddress NVARCHAR(500) NOT NULL
    );
END
GO

-- Create OrderItems Table
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'OrderItems')
BEGIN
    CREATE TABLE OrderItems (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        OrderId INT NOT NULL FOREIGN KEY REFERENCES Orders(Id),
        ProductId INT NOT NULL,
        ProductName NVARCHAR(255) NOT NULL,
        Price DECIMAL(18,2) NOT NULL,
        Quantity INT NOT NULL
    );
END
GO

-- Stored Procedure to Create an Order
CREATE OR ALTER PROCEDURE dbo.Order_Create
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
GO

-- Stored Procedure to Add an OrderItem
CREATE OR ALTER PROCEDURE dbo.OrderItem_Create
    @OrderId INT,
    @ProductId INT,
    @ProductName NVARCHAR(255),
    @Price DECIMAL(18,2),
    @Quantity INT
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO OrderItems (OrderId, ProductId, ProductName, Price, Quantity)
    VALUES (@OrderId, @ProductId, @ProductName, @Price, @Quantity);
END
GO
