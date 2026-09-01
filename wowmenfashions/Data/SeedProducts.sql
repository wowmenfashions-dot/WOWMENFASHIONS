-- Create Categories Table
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Categories')
BEGIN
    CREATE TABLE Categories (
        Id INT PRIMARY KEY,
        Name NVARCHAR(100) NOT NULL,
        Slug NVARCHAR(100) NOT NULL,
        ParentId INT NULL FOREIGN KEY REFERENCES Categories(Id)
    );
END

-- Create Products Table
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Products')
BEGIN
    CREATE TABLE Products (
        Id INT PRIMARY KEY,
        Name NVARCHAR(200) NOT NULL,
        CategoryId INT NOT NULL FOREIGN KEY REFERENCES Categories(Id),
        Category NVARCHAR(100) NOT NULL,
        Color NVARCHAR(50) NULL,
        Price DECIMAL(18,2) NOT NULL,
        OriginalPrice DECIMAL(18,2) NULL,
        ImageUrl NVARCHAR(500) NULL,
        Description NVARCHAR(MAX) NULL
    );
END
GO

-- Seed Data
IF NOT EXISTS (SELECT 1 FROM Categories)
BEGIN
    INSERT INTO Categories (Id, Name, Slug, ParentId) VALUES
    (1, 'Mens', 'mens', NULL),
    (2, 'Womens', 'womens', NULL),
    (3, 'Shirts', 'mens-shirts', 1),
    (4, 'Pants', 'mens-pants', 1),
    (5, 'Accessories', 'mens-accessories', 1),
    (6, 'Dresses', 'womens-dresses', 2),
    (7, 'Tops', 'womens-tops', 2),
    (8, 'Accessories', 'womens-accessories', 2);
END

IF NOT EXISTS (SELECT 1 FROM Products)
BEGIN
    INSERT INTO Products (Id, Name, CategoryId, Category, Color, Price, OriginalPrice, ImageUrl, Description) VALUES
    (1, 'Men''s Classic Blazer', 3, 'mens', 'Black', 1200.00, 1500.00, 'https://wowmenfashions.com/wp-content/uploads/2017/01/lookbook-grid-fourteen-1.jpg', 'A highly tailored, modern classic blazer perfect for both formal and smart-casual occasions. Made from premium wool blend.'),
    (2, 'Casual Denim Jacket', 3, 'mens', 'Blue', 800.00, NULL, 'https://wowmenfashions.com/wp-content/uploads/2017/01/lookbook-grid-fifteen-1.jpg', 'Rugged and durable denim jacket with a comfortable, slightly relaxed fit. A wardrobe essential.'),
    (3, 'Slim Fit Chinos', 4, 'mens', 'Burgundy', 650.00, 850.00, 'https://wowmenfashions.com/wp-content/uploads/2017/01/lookbook-grid-sixteen-1.jpg', 'Premium cotton chinos tailored for a slim, flattering fit with just the right amount of stretch.'),
    (4, 'Leather Wallet', 5, 'mens', 'Brown', 450.00, NULL, 'https://wowmenfashions.com/wp-content/uploads/2017/01/lookbook-grid-seventeen-1.jpg', 'Genuine leather bifold wallet with multiple card slots and a sleek profile.'),
    (5, 'Elegant Evening Dress', 6, 'womens', 'Black', 1800.00, 2200.00, 'https://wowmenfashions.com/wp-content/uploads/2017/01/lookbook-grid-seven-1.jpg', 'Stunning floor-length evening dress with delicate detailing, perfect for galas and formal events.'),
    (6, 'Summer Floral Top', 7, 'womens', 'White', 500.00, NULL, 'https://wowmenfashions.com/wp-content/uploads/2017/01/lookbook-grid-eight-1.jpg', 'Light and breezy floral top ideal for warm summer days. Pair it with jeans or a skirt.'),
    (7, 'Leather Handbag', 8, 'womens', 'Red', 1250.00, 1600.00, 'https://wowmenfashions.com/wp-content/uploads/2017/01/lookbook-grid-nine-1.jpg', 'Spacious and stylish genuine leather handbag featuring robust hardware and multiple compartments.'),
    (8, 'High Heel Shoes', 8, 'womens', 'Black', 950.00, NULL, 'https://wowmenfashions.com/wp-content/uploads/2017/01/lookbook-grid-ten-1.jpg', 'Classic black high heels that offer both elegance and unexpected comfort.');
END
GO

-- Stored Procedures
CREATE OR ALTER PROCEDURE Product_GetAllCategories
AS
BEGIN
    SELECT * FROM Categories;
END
GO

CREATE OR ALTER PROCEDURE Product_GetAllProducts
AS
BEGIN
    SELECT * FROM Products;
END
GO
