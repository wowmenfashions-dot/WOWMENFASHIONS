# Data Model: Admin, Products, and Images

## Entities

### `AdminUser`
Stores credentials for the administrative users.
- `Id` (INT, PK, IDENTITY)
- `Username` (NVARCHAR 50, UNIQUE)
- `PasswordHash` (NVARCHAR 255) - Stores hashed password (e.g., using BCrypt or similar standard).

### `ProductImage`
Stores multiple images for a given product.
- `Id` (INT, PK, IDENTITY)
- `ProductId` (INT, FK -> Products.Id)
- `ImageUrl` (NVARCHAR 1000)
- `IsPrimary` (BIT) - Indicates if this is the main image to display on cards. (Optional if we just use the existing `Products.ImageUrl` as the primary image, and use this table only for *additional* images. We will take the approach where `Products.ImageUrl` is the primary thumbnail, and `ProductImage` holds additional gallery images to avoid migrating existing primary images).

### `ProductDto` (C# Model Update)
- Add `public List<string> AdditionalImages { get; set; } = new();` to support the UI binding.

## Database Migrations / Seed Data
- Create table `AdminUsers`.
- Create table `ProductImages`.
- Insert a default admin user (`admin` / `password` for MVP).
- Insert sample secondary images for testing.
