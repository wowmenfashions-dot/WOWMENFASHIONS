CREATE TABLE [dbo].[Carts] (
    [Id]          UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    [CustomerId]  INT              NULL,
    [GuestCartId] UNIQUEIDENTIFIER NULL,
    [CreatedAt]   DATETIME2 (7)    DEFAULT (getutcdate()) NOT NULL,
    [UpdatedAt]   DATETIME2 (7)    DEFAULT (getutcdate()) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_Carts_CustomerId]
    ON [dbo].[Carts]([CustomerId] ASC) WHERE ([CustomerId] IS NOT NULL);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_Carts_GuestCartId]
    ON [dbo].[Carts]([GuestCartId] ASC) WHERE ([GuestCartId] IS NOT NULL);

