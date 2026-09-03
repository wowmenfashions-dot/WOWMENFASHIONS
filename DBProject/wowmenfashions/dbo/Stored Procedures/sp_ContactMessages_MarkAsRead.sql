
CREATE   PROCEDURE sp_ContactMessages_MarkAsRead
    @Id INT
AS
BEGIN
    UPDATE ContactMessages
    SET IsRead = 1
    WHERE Id = @Id
END