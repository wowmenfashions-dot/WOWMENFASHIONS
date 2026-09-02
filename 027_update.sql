USE wowmenfashions;
GO

-- Fix Bug: Alter ImageUrl column in Products table
ALTER TABLE [dbo].[Products] ALTER COLUMN [ImageUrl] NVARCHAR(MAX);
GO

-- Security: Create AdminAuditLogs table
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AdminAuditLogs]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[AdminAuditLogs](
        [Id] [int] IDENTITY(1,1) NOT NULL,
        [Action] [nvarchar](255) NOT NULL,
        [AdminEmail] [nvarchar](255) NOT NULL,
        [EntityId] [nvarchar](255) NULL,
        [Timestamp] [datetime] NOT NULL,
    CONSTRAINT [PK_AdminAuditLogs] PRIMARY KEY CLUSTERED 
    (
        [Id] ASC
    )
    )
END
GO

CREATE OR ALTER PROCEDURE sp_LogAdminAction
    @Action NVARCHAR(255),
    @AdminEmail NVARCHAR(255),
    @EntityId NVARCHAR(255)
AS
BEGIN
    INSERT INTO AdminAuditLogs (Action, AdminEmail, EntityId, Timestamp)
    VALUES (@Action, @AdminEmail, @EntityId, GETDATE())
END
GO
