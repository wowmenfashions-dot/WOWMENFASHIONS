CREATE TABLE [dbo].[Carts] (
    [Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT NEWID(),
    [CustomerId] INT NULL,
    [GuestCartId] UNIQUEIDENTIFIER NULL,
    [CreatedAt] DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
    [UpdatedAt] DATETIME2 NOT NULL DEFAULT GETUTCDATE()
);
GO

CREATE UNIQUE NONCLUSTERED INDEX [IX_Carts_GuestCartId]
ON [dbo].[Carts] ([GuestCartId])
WHERE [GuestCartId] IS NOT NULL;
GO

CREATE UNIQUE NONCLUSTERED INDEX [IX_Carts_CustomerId]
ON [dbo].[Carts] ([CustomerId])
WHERE [CustomerId] IS NOT NULL;
GO


CREATE TABLE [dbo].[CartItems] (
    [Id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [CartId] UNIQUEIDENTIFIER NOT NULL,
    [ProductId] INT NOT NULL,
    [Quantity] INT NOT NULL CHECK ([Quantity] > 0),
    [CreatedAt] DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
    [UpdatedAt] DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
    CONSTRAINT [FK_CartItems_Carts] FOREIGN KEY ([CartId]) REFERENCES [dbo].[Carts] ([Id]) ON DELETE CASCADE
    -- CONSTRAINT [FK_CartItems_Products] FOREIGN KEY ([ProductId]) REFERENCES [dbo].[Products] ([Id]) -- Uncomment when Products table exists
);
GO

CREATE UNIQUE NONCLUSTERED INDEX [IX_CartItems_CartId_ProductId]
ON [dbo].[CartItems] ([CartId], [ProductId]);
GO
