CREATE TABLE [dbo].[ContactMessages] (
    [Id]        INT            IDENTITY (1, 1) NOT NULL,
    [Name]      NVARCHAR (100) NOT NULL,
    [Email]     NVARCHAR (100) NOT NULL,
    [Subject]   NVARCHAR (200) NULL,
    [Message]   NVARCHAR (MAX) NOT NULL,
    [IsRead]    BIT            DEFAULT ((0)) NOT NULL,
    [CreatedAt] DATETIME       DEFAULT (getutcdate()) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

