# Research: Homepage Image Upload

## Decision 1: Caching Strategy
**Decision**: Use `IMemoryCache` in a Singleton `HomepageImageService`, or store the images in a `List<HomepageImage>` directly within a Singleton service.
**Rationale**: Since Blazor Server keeps a continuous process running, holding a small array of byte arrays (for 5-10 AVIF carousel images) in a Singleton service is extremely fast and incurs zero overhead compared to distributed caching or file I/O. We can initialize this cache on application startup or first request, and update it immediately whenever the admin saves the homepage configuration.
**Alternatives considered**: 
- Distributed Cache (Redis) - overkill for a single server instance with small payloads.
- Serving directly from Database - violates performance requirements.
- Writing to Disk (wwwroot) - requires complex file permission management and directory synchronization on redeployments; DB storage is more stateless and cloud-native.

## Decision 2: Image Conversion
**Decision**: Reuse or adapt `ImageUploadService` utilizing `SixLabors.ImageSharp`.
**Rationale**: The system already has a service for processing product images. We will configure it to output AVIF format and compress the payload.
**Alternatives considered**: SkiaSharp, System.Drawing (deprecated).
