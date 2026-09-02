USE wowmenfashions;
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PageContents]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[PageContents](
        [Id] [int] IDENTITY(1,1) NOT NULL,
        [PageName] [nvarchar](100) NOT NULL UNIQUE,
        [ContentHtml] [nvarchar](max) NOT NULL,
        [LastUpdated] [datetime] NOT NULL,
    CONSTRAINT [PK_PageContents] PRIMARY KEY CLUSTERED 
    (
        [Id] ASC
    )
    )
END
GO

CREATE OR ALTER PROCEDURE sp_PageContents_GetByName
    @PageName NVARCHAR(100)
AS
BEGIN
    SELECT Id, PageName, ContentHtml, LastUpdated
    FROM PageContents
    WHERE PageName = @PageName
END
GO

CREATE OR ALTER PROCEDURE sp_PageContents_Update
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
GO
