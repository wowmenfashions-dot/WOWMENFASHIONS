CREATE TABLE [dbo].[PageContents] (
    [Id]          INT            IDENTITY (1, 1) NOT NULL,
    [PageName]    NVARCHAR (100) NOT NULL,
    [ContentHtml] NVARCHAR (MAX) NOT NULL,
    [LastUpdated] DATETIME       NOT NULL,
    CONSTRAINT [PK_PageContents] PRIMARY KEY CLUSTERED ([Id] ASC),
    UNIQUE NONCLUSTERED ([PageName] ASC)
);

