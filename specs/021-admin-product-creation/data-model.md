# Data Model: admin-product-creation

## Entities

### Product (Existing)
- Id (INT)
- Name (NVARCHAR)
- Description (NVARCHAR)
- Price (DECIMAL)
- OriginalPrice (DECIMAL)
- Category (NVARCHAR)
- SubCategory (NVARCHAR)

### ProductColorImage (Existing)
- Id (INT)
- ProductId (INT)
- ColorName (NVARCHAR)
- ImageUrl (NVARCHAR)
- CreatedAt (DATETIME2)

## Validation Rules
- Product Name, Category, Price are required.
- If colors are specified, each color MUST have a name. Image URL is optional but recommended.
