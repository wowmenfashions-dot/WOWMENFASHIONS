CREATE TABLE [dbo].[ErrorLogs] (
    [Id]          INT            IDENTITY (1, 1) NOT NULL,
    [Message]     NVARCHAR (MAX) NOT NULL,
    [StackTrace]  NVARCHAR (MAX) NULL,
    [Timestamp]   DATETIME       DEFAULT (getutcdate()) NOT NULL,
    [UserContext] NVARCHAR (255) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

