# Implementation Tasks: Homepage Image Upload

## Phase 1: Database & Entities

- [ ] 1. Create `HomepageImage` entity class in `wowmenfashions/Data/Entities/HomepageImage.cs` with properties: `Id`, `ImageData` (byte[]), `ContentType` (string), and `DisplayOrder` (int).
- [ ] 2. Update `wowmenfashions/Data/ApplicationDbContext.cs` to include `DbSet<HomepageImage> HomepageImages`.
- [ ] 3. Create a migration to apply the new `HomepageImage` table. (e.g. `dotnet ef migrations add AddHomepageImages`, `dotnet ef database update`). Alternatively, write a raw SQL script if that's the project standard.

## Phase 2: Services & Caching

- [ ] 4. Create `IHomepageImageService.cs` and `HomepageImageService.cs` in `wowmenfashions/Services`.
- [ ] 5. Implement `Task<List<HomepageImage>> GetCachedImagesAsync()` in `HomepageImageService`. This method should check an internal `List<HomepageImage>` cache. If null/empty, fetch from DB ordered by `DisplayOrder`, populate the cache, and return.
- [ ] 6. Implement `Task SaveImagesAsync(List<HomepageImage> images)` which clears the `HomepageImages` table, inserts the new list, saves changes, and updates the in-memory cache directly.
- [ ] 7. Implement a method `Task<byte[]> ProcessImageAsync(Stream fileStream)` that utilizes the existing ImageSharp setup to compress and convert the stream into AVIF format. (Can be in `ImageUploadService` or `HomepageImageService`).
- [ ] 8. Register `HomepageImageService` as a Scoped or Singleton service in `Program.cs`. (If it caches data across users, it should be a Singleton, or use `IMemoryCache` inside a Scoped service).

## Phase 3: Admin UI Updates

- [ ] 9. Update `wowmenfashions/Components/Pages/AdminHomepageConfig.razor`. Replace the string-based URL list with a `MudFileUpload<IReadOnlyList<IBrowserFile>>`.
- [ ] 10. Implement logic in `AdminHomepageConfig.razor` to handle file uploads, preview them (optional, or just list names), allow re-ordering or deleting, and call `ProcessImageAsync` on each file to get the AVIF byte payload before passing the list to `HomepageImageService.SaveImagesAsync`.

## Phase 4: Storefront UI Updates

- [ ] 11. Create a generic API endpoint (e.g., in a minimal API or controller) `GET /api/homepage-images/{id}` to serve the images from the DB/cache since Blazor Server cannot easily serve byte arrays directly via standard `<img>` tags without a dedicated endpoint or using base64. Base64 is fine for small images but endpoints are better for caching. Alternatively, `src="data:image/avif;base64,..."` can be used directly in the Razor component since AVIF payloads are small and pre-cached in memory! Let's use `data:image/avif;base64,...` to avoid needing extra controllers and keep it purely Blazor.
- [ ] 12. Update `wowmenfashions/Components/Pages/Homepage.razor` to inject `IHomepageImageService`, load `GetCachedImagesAsync()` in `OnInitializedAsync`, and render the carousel using `data:image/avif;base64,@Convert.ToBase64String(image.ImageData)`.

## Phase 5: Cleanup & Polish

- [ ] 13. Test the upload flow and rendering flow to ensure AVIF images load instantly without DB overhead.
