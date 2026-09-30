-- ============================================================
-- WOWMENFASHIONS - Azure Database Migration Script
-- Date: 2026-09-27
-- Description: All schema changes required to sync Azure DB
--              with the latest application code.
-- Run this script ON YOUR AZURE DATABASE via the Query Editor.
-- ============================================================

-- ============================================================
-- STEP 1: Create HomepageImages table
-- ============================================================
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[HomepageImages]') AND type = N'U')
BEGIN
    CREATE TABLE [dbo].[HomepageImages](
        [Id] [int] IDENTITY(1,1) NOT NULL,
        [ImageData] [varbinary](max) NOT NULL,
        [ContentType] [nvarchar](50) NOT NULL DEFAULT 'image/avif',
        [DisplayOrder] [int] NOT NULL,
        CONSTRAINT [PK_HomepageImages] PRIMARY KEY CLUSTERED ([Id] ASC)
    );
    PRINT 'HomepageImages table created.';
END
ELSE
    PRINT 'HomepageImages table already exists. Skipped.';
GO

-- ============================================================
-- STEP 2: Add missing columns to Products table
-- ============================================================
IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[Products]') AND name = 'Specifications')
BEGIN
    ALTER TABLE [dbo].[Products] ADD [Specifications] NVARCHAR(MAX) NULL;
    PRINT 'Products.Specifications column added.';
END
ELSE
    PRINT 'Products.Specifications already exists. Skipped.';
GO

IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[Products]') AND name = 'ShippingReturns')
BEGIN
    ALTER TABLE [dbo].[Products] ADD [ShippingReturns] NVARCHAR(MAX) NULL;
    PRINT 'Products.ShippingReturns column added.';
END
ELSE
    PRINT 'Products.ShippingReturns already exists. Skipped.';
GO

IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[Products]') AND name = 'AvailableSizes')
BEGIN
    ALTER TABLE [dbo].[Products] ADD [AvailableSizes] NVARCHAR(255) NULL;
    PRINT 'Products.AvailableSizes column added.';
END
ELSE
    PRINT 'Products.AvailableSizes already exists. Skipped.';
GO

-- ============================================================
-- STEP 3: Create Reviews table
-- ============================================================
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Reviews]') AND type = N'U')
BEGIN
    CREATE TABLE [dbo].[Reviews](
        [Id] [int] IDENTITY(1,1) NOT NULL,
        [ProductId] [int] NOT NULL,
        [CustomerId] [int] NOT NULL,
        [Rating] [int] NOT NULL,
        [Comment] [nvarchar](max) NULL,
        [AdminReply] [nvarchar](max) NULL,
        [CreatedAt] [datetime2](7) NOT NULL DEFAULT (GETUTCDATE()),
        [UpdatedAt] [datetime2](7) NULL,
        CONSTRAINT [PK_Reviews] PRIMARY KEY CLUSTERED ([Id] ASC)
    );

    ALTER TABLE [dbo].[Reviews] ADD CONSTRAINT [FK_Reviews_Products]
        FOREIGN KEY([ProductId]) REFERENCES [dbo].[Products] ([Id]) ON DELETE CASCADE;

    ALTER TABLE [dbo].[Reviews] ADD CONSTRAINT [FK_Reviews_Customers]
        FOREIGN KEY([CustomerId]) REFERENCES [dbo].[Customers] ([Id]) ON DELETE CASCADE;

    PRINT 'Reviews table created.';
END
ELSE
    PRINT 'Reviews table already exists. Skipped.';
GO

-- ============================================================
-- STEP 4: Add SelectedSize column to CartItems
-- ============================================================
IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[CartItems]') AND name = 'SelectedSize')
BEGIN
    ALTER TABLE [dbo].[CartItems] ADD [SelectedSize] NVARCHAR(50) NULL;
    PRINT 'CartItems.SelectedSize column added.';
END
ELSE
    PRINT 'CartItems.SelectedSize already exists. Skipped.';
GO

-- ============================================================
-- STEP 5: Add SelectedSize column to OrderItems
-- ============================================================
IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[OrderItems]') AND name = 'SelectedSize')
BEGIN
    ALTER TABLE [dbo].[OrderItems] ADD [SelectedSize] NVARCHAR(50) NULL;
    PRINT 'OrderItems.SelectedSize column added.';
END
ELSE
    PRINT 'OrderItems.SelectedSize already exists. Skipped.';
GO

-- ============================================================
-- STEP 6: Update Cart_AddItem stored procedure
-- ============================================================
CREATE OR ALTER PROCEDURE [dbo].[Cart_AddItem]
    @GuestCartId UNIQUEIDENTIFIER = NULL,
    @CustomerId INT = NULL,
    @ProductId INT,
    @Quantity INT,
    @SelectedColor NVARCHAR(50) = NULL,
    @SelectedSize NVARCHAR(50) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @CartId UNIQUEIDENTIFIER;

    IF @CustomerId IS NOT NULL
        SELECT @CartId = Id FROM Carts WHERE CustomerId = @CustomerId;
    ELSE IF @GuestCartId IS NOT NULL
        SELECT @CartId = Id FROM Carts WHERE GuestCartId = @GuestCartId;

    IF @CartId IS NULL
    BEGIN
        SET @CartId = NEWID();
        IF @GuestCartId IS NULL AND @CustomerId IS NULL
            SET @GuestCartId = NEWID();
        INSERT INTO Carts (Id, CustomerId, GuestCartId, CreatedAt, UpdatedAt)
        VALUES (@CartId, @CustomerId, @GuestCartId, GETUTCDATE(), GETUTCDATE());
    END

    IF EXISTS (
        SELECT 1 FROM CartItems
        WHERE CartId = @CartId
          AND ProductId = @ProductId
          AND ISNULL(SelectedColor, '') = ISNULL(@SelectedColor, '')
          AND ISNULL(SelectedSize, '') = ISNULL(@SelectedSize, '')
    )
    BEGIN
        UPDATE CartItems
        SET Quantity = Quantity + @Quantity, UpdatedAt = GETUTCDATE()
        WHERE CartId = @CartId
          AND ProductId = @ProductId
          AND ISNULL(SelectedColor, '') = ISNULL(@SelectedColor, '')
          AND ISNULL(SelectedSize, '') = ISNULL(@SelectedSize, '');
    END
    ELSE
    BEGIN
        INSERT INTO CartItems (CartId, ProductId, Quantity, CreatedAt, UpdatedAt, SelectedColor, SelectedSize)
        VALUES (@CartId, @ProductId, @Quantity, GETUTCDATE(), GETUTCDATE(), @SelectedColor, @SelectedSize);
    END

    SELECT @CartId AS CartId, @GuestCartId AS GuestCartId;
END
GO
PRINT 'Cart_AddItem stored procedure updated.';
GO

-- ============================================================
-- STEP 7: Update OrderItem_Create stored procedure
-- ============================================================
CREATE OR ALTER PROCEDURE [dbo].[OrderItem_Create]
    @OrderId INT,
    @ProductId INT,
    @ProductName NVARCHAR(255),
    @Price DECIMAL(18,2),
    @Quantity INT,
    @SelectedColor NVARCHAR(50) = NULL,
    @SelectedSize NVARCHAR(50) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO OrderItems (OrderId, ProductId, ProductName, Price, Quantity, SelectedColor, SelectedSize)
    VALUES (@OrderId, @ProductId, @ProductName, @Price, @Quantity, @SelectedColor, @SelectedSize);
END
GO
PRINT 'OrderItem_Create stored procedure updated.';
GO

PRINT '============================================================';
PRINT 'Migration complete! All 7 steps applied successfully.';
PRINT '============================================================';
