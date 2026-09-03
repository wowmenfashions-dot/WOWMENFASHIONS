CREATE TABLE [dbo].[AdminAuditLogs] (
    [Id]         INT            IDENTITY (1, 1) NOT NULL,
    [Action]     NVARCHAR (255) NOT NULL,
    [AdminEmail] NVARCHAR (255) NOT NULL,
    [EntityId]   NVARCHAR (255) NULL,
    [Timestamp]  DATETIME       NOT NULL,
    CONSTRAINT [PK_AdminAuditLogs] PRIMARY KEY CLUSTERED ([Id] ASC)
);

