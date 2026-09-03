CREATE TABLE [dbo].[CartItems] (
    [Id]            INT              IDENTITY (1, 1) NOT NULL,
    [CartId]        UNIQUEIDENTIFIER NOT NULL,
    [ProductId]     INT              NOT NULL,
    [Quantity]      INT              NOT NULL,
    [CreatedAt]     DATETIME2 (7)    DEFAULT (getutcdate()) NOT NULL,
    [UpdatedAt]     DATETIME2 (7)    DEFAULT (getutcdate()) NOT NULL,
    [SelectedColor] NVARCHAR (50)    NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CHECK ([Quantity]>(0)),
    CONSTRAINT [FK_CartItems_Carts] FOREIGN KEY ([CartId]) REFERENCES [dbo].[Carts] ([Id]) ON DELETE CASCADE
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_CartItems_CartId_ProductId_Color]
    ON [dbo].[CartItems]([CartId] ASC, [ProductId] ASC, [SelectedColor] ASC);

