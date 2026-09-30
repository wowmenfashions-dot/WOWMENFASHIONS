-- ============================================================
-- Migration 036: Create EmailNotificationLogs table
-- Date: 2026-09-27
-- Feature: 037-email-notifications
-- Run this on BOTH local and Azure databases.
-- ============================================================

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EmailNotificationLogs]') AND type = N'U')
BEGIN
    CREATE TABLE [dbo].[EmailNotificationLogs](
        [Id] [int] IDENTITY(1,1) NOT NULL,
        [OrderId] [int] NOT NULL,
        [NotificationType] [nvarchar](50) NOT NULL,
        [RecipientEmail] [nvarchar](256) NOT NULL,
        [Status] [nvarchar](20) NOT NULL,
        [SentAt] [datetime2](7) NOT NULL DEFAULT (GETUTCDATE()),
        [ErrorMessage] [nvarchar](max) NULL,
        [OrderStatus] [nvarchar](50) NULL,
        CONSTRAINT [PK_EmailNotificationLogs] PRIMARY KEY CLUSTERED ([Id] ASC)
    );
    PRINT 'EmailNotificationLogs table created.';
END
ELSE
    PRINT 'EmailNotificationLogs table already exists. Skipped.';
GO
