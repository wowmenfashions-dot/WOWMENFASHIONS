CREATE TABLE [dbo].[Reviews](
	[Id] [int] IDENTITY(1,1) PRIMARY KEY,
	[ProductId] [int] NOT NULL,
	[CustomerId] [int] NOT NULL,
	[Rating] [int] NOT NULL,
	[Comment] [nvarchar](max) NULL,
	[AdminReply] [nvarchar](max) NULL,
	[CreatedAt] [datetime2](7) NOT NULL DEFAULT (GETUTCDATE()),
	[UpdatedAt] [datetime2](7) NULL
);
GO

ALTER TABLE [dbo].[Reviews] ADD CONSTRAINT [FK_Reviews_Products] FOREIGN KEY([ProductId]) REFERENCES [dbo].[Products] ([Id]) ON DELETE CASCADE;
GO

ALTER TABLE [dbo].[Reviews] ADD CONSTRAINT [FK_Reviews_Customers] FOREIGN KEY([CustomerId]) REFERENCES [dbo].[Customers] ([Id]) ON DELETE CASCADE;
GO
