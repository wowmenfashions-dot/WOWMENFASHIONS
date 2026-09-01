-- 02_SeedDummyData.sql
-- Seed script for categories and dummy products (AVIF images only)

SET NOCOUNT ON;

-- 1. Insert Categories if they don't exist
DECLARE @Categories TABLE (Name NVARCHAR(100), Slug NVARCHAR(100), ProductCount INT);
INSERT INTO @Categories (Name, Slug, ProductCount)
VALUES 
    ('Mens', 'mens', 100),
    ('Womens', 'womens', 100),
    ('Kids', 'kids', 50),
    ('Toddler', 'toddler', 50),
    ('Adults', 'adults', 50),
    ('Teenager', 'teenager', 50);

DECLARE @CatName NVARCHAR(100), @CatSlug NVARCHAR(100), @ProductCount INT, @CategoryId INT;
DECLARE @i INT, @Price DECIMAL(18,2), @OriginalPrice DECIMAL(18,2), @HasSale BIT;
DECLARE @Color NVARCHAR(50), @ColorRand INT;
DECLARE @Colors TABLE (Id INT, ColorName NVARCHAR(50));
INSERT INTO @Colors VALUES (1, 'Red'), (2, 'Blue'), (3, 'Black'), (4, 'White'), (5, 'Any Color');

DECLARE @NextId INT;
SELECT @NextId = ISNULL(MAX(Id), 0) FROM Products;

DECLARE cat_cursor CURSOR FOR SELECT Name, Slug, ProductCount FROM @Categories;
OPEN cat_cursor;
FETCH NEXT FROM cat_cursor INTO @CatName, @CatSlug, @ProductCount;

WHILE @@FETCH_STATUS = 0
BEGIN
    -- Check if category exists
    SELECT @CategoryId = Id FROM Categories WHERE Slug = @CatSlug;
    
    IF @CategoryId IS NULL
    BEGIN
        INSERT INTO Categories (Name, Slug) VALUES (@CatName, @CatSlug);
        SET @CategoryId = SCOPE_IDENTITY();
    END

    -- Insert products for this category
    SET @i = 1;
    WHILE @i <= @ProductCount
    BEGIN
        SET @NextId = @NextId + 1;
        -- Randomize data
        SET @Price = ROUND((RAND() * (200.00 - 10.00) + 10.00), 2);
        SET @HasSale = CAST(ROUND(RAND(), 0) AS BIT);
        
        IF @HasSale = 1
            SET @OriginalPrice = @Price + ROUND((RAND() * 50.00 + 10.00), 2);
        ELSE
            SET @OriginalPrice = NULL;

        SET @ColorRand = CAST((RAND() * 4) + 1 AS INT);
        SELECT @Color = ColorName FROM @Colors WHERE Id = @ColorRand;

        INSERT INTO Products (
            Id,
            CategoryId, 
            Category,
            Name, 
            Description, 
            Price, 
            OriginalPrice, 
            ImageUrl, 
            Color
        )
        VALUES (
            @NextId,
            @CategoryId,
            @CatName,
            @CatName + ' Product ' + CAST(@i AS NVARCHAR(10)),
            'This is a generated dummy product for the ' + @CatName + ' category. Features random styling and an AVIF image.',
            @Price,
            @OriginalPrice,
            'https://picsum.photos/seed/' + CAST(NEWID() AS NVARCHAR(36)) + '/400/400.avif',
            @Color
        );

        SET @i = @i + 1;
    END

    FETCH NEXT FROM cat_cursor INTO @CatName, @CatSlug, @ProductCount;
END

CLOSE cat_cursor;
DEALLOCATE cat_cursor;

PRINT 'Dummy data seeding completed successfully. Generated 400 AVIF products across 6 categories.';
