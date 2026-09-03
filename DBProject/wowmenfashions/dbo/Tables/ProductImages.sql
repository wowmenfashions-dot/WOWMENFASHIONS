CREATE TABLE [dbo].[ProductImages] (
    [Id]        INT             IDENTITY (1, 1) NOT NULL,
    [ProductId] INT             NOT NULL,
    [ImageUrl]  NVARCHAR (1000) NOT NULL,
    [IsPrimary] BIT             DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_ProductImages] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ProductImages_Products] FOREIGN KEY ([ProductId]) REFERENCES [dbo].[Products] ([Id]) ON DELETE CASCADE
);

