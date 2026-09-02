IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='ContactMessages' and xtype='U')
BEGIN
    CREATE TABLE ContactMessages (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        Name NVARCHAR(100) NOT NULL,
        Email NVARCHAR(100) NOT NULL,
        Subject NVARCHAR(200) NULL,
        Message NVARCHAR(MAX) NOT NULL,
        IsRead BIT NOT NULL DEFAULT 0,
        CreatedAt DATETIME NOT NULL DEFAULT GETUTCDATE()
    )
END
GO

CREATE OR ALTER PROCEDURE sp_ContactMessages_Insert
    @Name NVARCHAR(100),
    @Email NVARCHAR(100),
    @Subject NVARCHAR(200),
    @Message NVARCHAR(MAX)
AS
BEGIN
    INSERT INTO ContactMessages (Name, Email, Subject, Message, IsRead, CreatedAt)
    VALUES (@Name, @Email, @Subject, @Message, 0, GETUTCDATE())
    
    SELECT CAST(SCOPE_IDENTITY() as int)
END
GO

CREATE OR ALTER PROCEDURE sp_ContactMessages_GetAll
AS
BEGIN
    SELECT * FROM ContactMessages
    ORDER BY CreatedAt DESC
END
GO

CREATE OR ALTER PROCEDURE sp_ContactMessages_MarkAsRead
    @Id INT
AS
BEGIN
    UPDATE ContactMessages
    SET IsRead = 1
    WHERE Id = @Id
END
GO
