
CREATE   PROCEDURE sp_ContactMessages_Insert
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