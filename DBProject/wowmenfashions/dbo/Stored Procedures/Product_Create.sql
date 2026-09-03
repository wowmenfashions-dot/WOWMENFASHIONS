CREATE   PROCEDURE [dbo].[Product_Create]
    @Name NVARCHAR(MAX),
    @Description NVARCHAR(MAX),
    @Price DECIMAL(18, 2),
    @OriginalPrice DECIMAL(18, 2),
    @Category NVARCHAR(MAX)
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO Products (Name, Description, Price, OriginalPrice, Category, ImageUrl)
    VALUES (@Name, @Description, @Price, @OriginalPrice, @Category, '/images/dummy-product.avif');

    SELECT SCOPE_IDENTITY() AS ProductId;
END