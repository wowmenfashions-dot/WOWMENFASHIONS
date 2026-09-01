CREATE OR ALTER PROCEDURE [dbo].[ProductColorImage_Create]
    @ProductId INT,
    @ColorName NVARCHAR(50),
    @ImageUrl NVARCHAR(MAX)
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO ProductColorImages (ProductId, ColorName, ImageUrl)
    VALUES (@ProductId, @ColorName, @ImageUrl);
END
