CREATE TABLE [dbo].[AdminUsers] (
    [Id]           INT            IDENTITY (1, 1) NOT NULL,
    [Username]     NVARCHAR (50)  NOT NULL,
    [PasswordHash] NVARCHAR (255) NOT NULL,
    CONSTRAINT [PK_AdminUsers] PRIMARY KEY CLUSTERED ([Id] ASC),
    UNIQUE NONCLUSTERED ([Username] ASC)
);

