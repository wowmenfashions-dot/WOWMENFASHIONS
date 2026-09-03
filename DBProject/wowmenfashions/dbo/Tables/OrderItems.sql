CREATE TABLE [dbo].[OrderItems] (
    [Id]            INT             IDENTITY (1, 1) NOT NULL,
    [OrderId]       INT             NOT NULL,
    [ProductId]     INT             NOT NULL,
    [ProductName]   NVARCHAR (255)  NOT NULL,
    [Price]         DECIMAL (18, 2) NOT NULL,
    [Quantity]      INT             NOT NULL,
    [SelectedColor] NVARCHAR (50)   NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    FOREIGN KEY ([OrderId]) REFERENCES [dbo].[Orders] ([Id])
);

