-- Create CustomerAddresses Table
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'CustomerAddresses')
BEGIN
    CREATE TABLE CustomerAddresses (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        CustomerId INT NOT NULL FOREIGN KEY REFERENCES Customers(Id),
        FullName NVARCHAR(100) NOT NULL,
        AddressLine NVARCHAR(200) NOT NULL,
        City NVARCHAR(100) NOT NULL,
        PostalCode NVARCHAR(20) NOT NULL,
        Country NVARCHAR(100) NOT NULL,
        IsDefaultShipping BIT NOT NULL DEFAULT 0,
        IsDefaultBilling BIT NOT NULL DEFAULT 0
    );
END
GO

-- Add AvailableColors to Products
IF NOT EXISTS (
  SELECT * 
  FROM INFORMATION_SCHEMA.COLUMNS 
  WHERE TABLE_NAME = 'Products' AND COLUMN_NAME = 'AvailableColors'
)
BEGIN
    ALTER TABLE Products ADD AvailableColors NVARCHAR(MAX) NULL;
END
GO

-- Add SelectedColor to CartItems
IF NOT EXISTS (
  SELECT * 
  FROM INFORMATION_SCHEMA.COLUMNS 
  WHERE TABLE_NAME = 'CartItems' AND COLUMN_NAME = 'SelectedColor'
)
BEGIN
    ALTER TABLE CartItems ADD SelectedColor NVARCHAR(50) NULL;
END
GO

-- Create Orders Table (if not exists from previous checkout feature)
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Orders')
BEGIN
    CREATE TABLE Orders (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        CustomerId INT NULL FOREIGN KEY REFERENCES Customers(Id),
        CustomerEmail NVARCHAR(256) NOT NULL,
        CustomerName NVARCHAR(100) NOT NULL,
        ShippingAddress NVARCHAR(MAX) NOT NULL,
        OrderDate DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
        Subtotal DECIMAL(18,2) NOT NULL,
        ShippingFee DECIMAL(18,2) NOT NULL,
        TaxAmount DECIMAL(18,2) NOT NULL,
        DiscountAmount DECIMAL(18,2) NOT NULL,
        TotalAmount DECIMAL(18,2) NOT NULL,
        Status NVARCHAR(50) NOT NULL
    );
END
GO

-- Create OrderItems Table (if not exists)
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'OrderItems')
BEGIN
    CREATE TABLE OrderItems (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        OrderId INT NOT NULL FOREIGN KEY REFERENCES Orders(Id),
        ProductId INT NOT NULL FOREIGN KEY REFERENCES Products(Id),
        ProductName NVARCHAR(200) NOT NULL,
        Price DECIMAL(18,2) NOT NULL,
        Quantity INT NOT NULL,
        SelectedColor NVARCHAR(50) NULL
    );
END
ELSE
BEGIN
    -- Add SelectedColor to OrderItems if it already existed without it
    IF NOT EXISTS (
      SELECT * 
      FROM INFORMATION_SCHEMA.COLUMNS 
      WHERE TABLE_NAME = 'OrderItems' AND COLUMN_NAME = 'SelectedColor'
    )
    BEGIN
        ALTER TABLE OrderItems ADD SelectedColor NVARCHAR(50) NULL;
    END
END
GO
