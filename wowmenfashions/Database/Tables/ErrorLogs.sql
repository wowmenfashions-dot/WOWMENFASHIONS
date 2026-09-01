IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='ErrorLogs' and xtype='U')
BEGIN
    CREATE TABLE ErrorLogs (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        Message NVARCHAR(MAX) NOT NULL,
        StackTrace NVARCHAR(MAX) NULL,
        Timestamp DATETIME NOT NULL DEFAULT GETUTCDATE(),
        UserContext NVARCHAR(255) NULL
    );
END
