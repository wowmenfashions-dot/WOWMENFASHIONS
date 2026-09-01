-- Task 1: Store UX Updates Schema Changes

-- 1. Create CustomerAddresses table
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CustomerAddresses]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[CustomerAddresses] (
        [Id] INT IDENTITY(1,1) PRIMARY KEY,
        [CustomerId] INT NOT NULL,
        [FullName] NVARCHAR(100) NOT NULL,
        [AddressLine] NVARCHAR(255) NOT NULL,
        [City] NVARCHAR(100) NOT NULL,
        [PostalCode] NVARCHAR(20) NOT NULL,
        [Country] NVARCHAR(100) NOT NULL,
        [IsDefaultShipping] BIT NOT NULL DEFAULT 0,
        [IsDefaultBilling] BIT NOT NULL DEFAULT 0,
        [CreatedAt] DATETIME2 NOT NULL DEFAULT GETDATE(),
        [UpdatedAt] DATETIME2 NOT NULL DEFAULT GETDATE(),
        CONSTRAINT [FK_CustomerAddresses_Customers] FOREIGN KEY ([CustomerId]) REFERENCES [dbo].[Customers]([Id]) ON DELETE CASCADE
    );
END
GO

-- 2. Add AvailableColors to Products
IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[Products]') AND name = 'AvailableColors')
BEGIN
    ALTER TABLE [dbo].[Products] ADD [AvailableColors] NVARCHAR(255) NULL;
END
GO

-- Migrate existing Color column into AvailableColors
UPDATE [dbo].[Products] SET [AvailableColors] = [Color] WHERE [AvailableColors] IS NULL AND [Color] IS NOT NULL;
GO

-- 3. Add SelectedColor to CartItems
IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[CartItems]') AND name = 'SelectedColor')
BEGIN
    ALTER TABLE [dbo].[CartItems] ADD [SelectedColor] NVARCHAR(50) NULL;
END
GO

-- 4. Add SelectedColor to OrderItems
IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[OrderItems]') AND name = 'SelectedColor')
BEGIN
    ALTER TABLE [dbo].[OrderItems] ADD [SelectedColor] NVARCHAR(50) NULL;
END
GO

-- Update Cart_AddItem stored procedure to accept color
CREATE OR ALTER PROCEDURE [dbo].[Cart_AddItem]
    @GuestCartId UNIQUEIDENTIFIER = NULL,
    @CustomerId INT = NULL,
    @ProductId INT,
    @Quantity INT,
    @SelectedColor NVARCHAR(50) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @CartId UNIQUEIDENTIFIER;

    IF @CustomerId IS NOT NULL
    BEGIN
        SELECT @CartId = Id FROM Carts WHERE CustomerId = @CustomerId;
        IF @CartId IS NULL
        BEGIN
            SET @CartId = NEWID();
            INSERT INTO Carts (Id, CustomerId, CreatedAt, UpdatedAt) VALUES (@CartId, @CustomerId, GETUTCDATE(), GETUTCDATE());
        END
    END
    ELSE IF @GuestCartId IS NOT NULL
    BEGIN
        SELECT @CartId = Id FROM Carts WHERE GuestCartId = @GuestCartId;
        IF @CartId IS NULL
        BEGIN
            SET @CartId = @GuestCartId;
            INSERT INTO Carts (Id, GuestCartId, CreatedAt, UpdatedAt) VALUES (@CartId, @GuestCartId, GETUTCDATE(), GETUTCDATE());
        END
    END
    ELSE
    BEGIN
        RETURN;
    END

    -- Check if item exists in cart (matching Product AND Color)
    IF EXISTS (SELECT 1 FROM CartItems WHERE CartId = @CartId AND ProductId = @ProductId AND ISNULL(SelectedColor, '') = ISNULL(@SelectedColor, ''))
    BEGIN
        UPDATE CartItems 
        SET Quantity = Quantity + @Quantity, UpdatedAt = GETUTCDATE()
        WHERE CartId = @CartId AND ProductId = @ProductId AND ISNULL(SelectedColor, '') = ISNULL(@SelectedColor, '');
    END
    ELSE
    BEGIN
        INSERT INTO CartItems (CartId, ProductId, Quantity, SelectedColor, CreatedAt, UpdatedAt)
        VALUES (@CartId, @ProductId, @Quantity, @SelectedColor, GETUTCDATE(), GETUTCDATE());
    END
END
GO
