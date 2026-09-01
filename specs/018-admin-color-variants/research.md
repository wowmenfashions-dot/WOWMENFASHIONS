# Research Notes: Admin Product Color Variants

## Database Schema Structure
- **Decision**: Create a new entity `ProductColorImage` which maps `ProductId`, `ColorName`, and `ImageUrl`.
- **Rationale**: A one-to-many relationship from `Product` to `ProductColorImage` is the cleanest normalized way to store this mapping in EF Core.
- **Alternatives considered**: Storing a JSON string in a single column in the `Product` table (rejected due to complexity with EF Core querying).

## Image Uploads
- **Decision**: Utilize the existing `IWebHostEnvironment` implementation used for standard Product Image uploads to handle Color Variant image uploads, saving them to `wwwroot/images/variants/` or `wwwroot/images/banners/`.
- **Rationale**: Keeps image management consistent.
