CREATE TABLE [dbo].[Products] (
    [Id]              INT             NOT NULL,
    [Name]            NVARCHAR (200)  NOT NULL,
    [CategoryId]      INT             NOT NULL,
    [Category]        NVARCHAR (100)  NOT NULL,
    [Color]           NVARCHAR (50)   NULL,
    [Price]           DECIMAL (18, 2) NOT NULL,
    [OriginalPrice]   DECIMAL (18, 2) NULL,
    [ImageUrl]        NVARCHAR (MAX)  NULL,
    [Description]     NVARCHAR (MAX)  NULL,
    [AvailableColors] NVARCHAR (255)  NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    FOREIGN KEY ([CategoryId]) REFERENCES [dbo].[Categories] ([Id])
);

