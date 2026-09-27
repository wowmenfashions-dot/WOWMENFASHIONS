# Data Model

*Note: This feature primarily addresses UI/UX updates. The underlying database schema and models are not expected to change significantly. The existing models are referenced below for context.*

## Entities

### `Product`
Represents an item in the store catalog.
- `Id` (int): Unique identifier.
- `Name` (string): The name of the product.
- `Description` (string): Detailed product information.
- `Price` (decimal): The current price.
- `Images` (List<ProductImage>): Collection of associated images.
- `Colors` (List<ProductColor>): Available color variants.

### `ProductImage`
Represents an image asset for a product.
- `Id` (int): Unique identifier.
- `ProductId` (int): Foreign key to the parent product.
- `ImageUrl` (string): Path or URL to the image (MUST be AVIF format).
- `IsPrimary` (bool): Indicates the main image.

### `ProductColor`
Represents a color variant for a product.
- `Id` (int): Unique identifier.
- `ProductId` (int): Foreign key to the parent product.
- `ColorName` (string): Friendly name (e.g., "Navy").
- `HexCode` (string): CSS hex color code (e.g., "#000080").
