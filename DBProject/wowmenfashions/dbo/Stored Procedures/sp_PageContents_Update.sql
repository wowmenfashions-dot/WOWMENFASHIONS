
CREATE   PROCEDURE sp_PageContents_Update
    @PageName NVARCHAR(100),
    @ContentHtml NVARCHAR(MAX)
AS
BEGIN
    IF EXISTS (SELECT 1 FROM PageContents WHERE PageName = @PageName)
    BEGIN
        UPDATE PageContents
        SET ContentHtml = @ContentHtml,
            LastUpdated = GETDATE()
        WHERE PageName = @PageName
    END
    ELSE
    BEGIN
        INSERT INTO PageContents (PageName, ContentHtml, LastUpdated)
        VALUES (@PageName, @ContentHtml, GETDATE())
    END
END