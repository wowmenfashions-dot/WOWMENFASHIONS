CREATE TABLE [dbo].[ProductColorImages] (
    [Id]        INT            IDENTITY (1, 1) NOT NULL,
    [ProductId] INT            NOT NULL,
    [ColorName] NVARCHAR (50)  NOT NULL,
    [ImageUrl]  NVARCHAR (500) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    FOREIGN KEY ([ProductId]) REFERENCES [dbo].[Products] ([Id]) ON DELETE CASCADE
);

