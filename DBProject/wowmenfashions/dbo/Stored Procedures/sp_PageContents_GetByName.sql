
CREATE   PROCEDURE sp_PageContents_GetByName
    @PageName NVARCHAR(100)
AS
BEGIN
    SELECT Id, PageName, ContentHtml, LastUpdated
    FROM PageContents
    WHERE PageName = @PageName
END