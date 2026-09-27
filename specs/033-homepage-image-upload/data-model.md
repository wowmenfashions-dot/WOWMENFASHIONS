# Data Model: Homepage Image Upload

## Entities

### `HomepageImage`
Stores the AVIF payloads for homepage carousel images in the database.

**Fields**:
- `Id` (int, Primary Key)
- `ImageData` (byte[], Required) - The compressed AVIF payload.
- `ContentType` (string, Required) - Typically "image/avif".
- `DisplayOrder` (int, Required) - The order in which the image appears in the carousel.

## Relationships
- This entity stands alone. It replaces the previous method of storing URLs in a generic settings JSON/string format, or integrates alongside it specifically for carousel images.

## State Management
- `HomepageImageService` will maintain a `List<HomepageImage>` in memory.
- `LoadImagesAsync()` reads all records ordered by `DisplayOrder` into memory.
- `SaveImagesAsync(List<HomepageImage>)` clears the existing table and inserts the new list, then immediately updates the in-memory cache.
