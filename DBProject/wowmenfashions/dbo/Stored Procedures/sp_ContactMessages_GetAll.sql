
CREATE   PROCEDURE sp_ContactMessages_GetAll
AS
BEGIN
    SELECT * FROM ContactMessages
    ORDER BY CreatedAt DESC
END