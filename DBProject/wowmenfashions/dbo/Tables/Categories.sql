CREATE TABLE [dbo].[Categories] (
    [Id]       INT            NOT NULL,
    [Name]     NVARCHAR (100) NOT NULL,
    [Slug]     NVARCHAR (100) NOT NULL,
    [ParentId] INT            NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    FOREIGN KEY ([ParentId]) REFERENCES [dbo].[Categories] ([Id])
);

