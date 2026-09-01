# Data Model: dynamic-homepage

## Entities

### HomepageConfig
- `Id` (INT, Primary Key)
- `ConfigKey` (NVARCHAR(100), Unique) - Identifier for the config section (e.g., "Carousel", "Announcement", "FeaturedProducts")
- `ConfigValue` (NVARCHAR(MAX)) - A JSON serialized string representing the content (e.g., list of image URLs, announcement text).
- `UpdatedAt` (DATETIME2) - Timestamp of last update.
