USE wowmenfashions;
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AdminUsers]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[AdminUsers](
        [Id] [int] IDENTITY(1,1) NOT NULL,
        [Username] [nvarchar](50) NOT NULL UNIQUE,
        [PasswordHash] [nvarchar](255) NOT NULL,
        CONSTRAINT [PK_AdminUsers] PRIMARY KEY CLUSTERED ([Id] ASC)
    )
END
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ProductImages]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[ProductImages](
        [Id] [int] IDENTITY(1,1) NOT NULL,
        [ProductId] [int] NOT NULL,
        [ImageUrl] [nvarchar](1000) NOT NULL,
        [IsPrimary] [bit] NOT NULL DEFAULT 0,
        CONSTRAINT [PK_ProductImages] PRIMARY KEY CLUSTERED ([Id] ASC),
        CONSTRAINT [FK_ProductImages_Products] FOREIGN KEY([ProductId])
        REFERENCES [dbo].[Products] ([Id])
        ON DELETE CASCADE
    )
END
GO

-- Seed Admin (Password is plain text for MVP, should be hashed in prod)
IF NOT EXISTS (SELECT * FROM AdminUsers WHERE Username = 'admin')
BEGIN
    INSERT INTO AdminUsers (Username, PasswordHash) VALUES ('admin', 'password')
END
GO

-- Seed some extra images for a few products to demonstrate the carousel
INSERT INTO ProductImages (ProductId, ImageUrl, IsPrimary)
SELECT Id, 'https://picsum.photos/seed/' + CAST(NEWID() AS NVARCHAR(36)) + '/400/400.avif', 0
FROM Products 
WHERE Id <= 10;

INSERT INTO ProductImages (ProductId, ImageUrl, IsPrimary)
SELECT Id, 'https://picsum.photos/seed/' + CAST(NEWID() AS NVARCHAR(36)) + '/400/400.avif', 0
FROM Products 
WHERE Id <= 10;
GO
