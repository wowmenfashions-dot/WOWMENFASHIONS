CREATE TABLE [dbo].[Customers] (
    [Id]              INT            IDENTITY (1, 1) NOT NULL,
    [FirstName]       NVARCHAR (100) NOT NULL,
    [LastName]        NVARCHAR (100) NOT NULL,
    [Email]           NVARCHAR (256) NOT NULL,
    [Phone]           NVARCHAR (20)  NULL,
    [PasswordHash]    NVARCHAR (MAX) NOT NULL,
    [IsEmailVerified] BIT            DEFAULT ((0)) NOT NULL,
    [IsActive]        BIT            DEFAULT ((1)) NOT NULL,
    [CreatedAt]       DATETIME2 (7)  DEFAULT (getutcdate()) NOT NULL,
    [UpdatedAt]       DATETIME2 (7)  DEFAULT (getutcdate()) NOT NULL,
    [LastLoginAt]     DATETIME2 (7)  NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_Customers_Email]
    ON [dbo].[Customers]([Email] ASC);

