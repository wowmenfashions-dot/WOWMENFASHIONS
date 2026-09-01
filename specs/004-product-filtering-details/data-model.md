# Data Model: Product Filtering & Details

## Entities

### `CategoryDto`
Represents a category that can be part of a hierarchy.
- `Id` (int)
- `Name` (string)
- `Slug` (string)
- `ParentId` (int?) - Null if it's a root category (e.g., Mens). If populated, it belongs to a parent (e.g., Shirts).

### `ProductDto` (Modifications)
Enhancements to the existing Dto for the details page and cascading filters.
- `Id` (int)
- `Name` (string)
- `Price` (decimal)
- `OriginalPrice` (decimal?)
- `ImageUrl` (string)
- `Category` (string) - Deprecated/retained for backward compatibility.
- `CategoryId` (int) - NEW: The specific category/subcategory the product belongs to.
- `Description` (string) - NEW: Full descriptive text for the details page.
