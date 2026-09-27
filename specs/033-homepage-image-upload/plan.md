# Implementation Plan: Homepage Image Upload

**Branch**: `033-homepage-image-upload` | **Date**: 2026-09-27 | **Spec**: [specs/033-homepage-image-upload/spec.md](file:///c:/MyDrive/ProjectDrive/WOWMENFASHIONS/specs/033-homepage-image-upload/spec.md)

**Input**: Feature specification from `/specs/033-homepage-image-upload/spec.md`

## Summary

This feature replaces URL textboxes with image file uploads for the admin homepage carousel. Uploaded images will be converted to AVIF, compressed, stored in the database as byte payloads, and served to the storefront directly from an in-memory application cache to optimize load times and eliminate database overhead on every request.

## Technical Context

**Language/Version**: C# / .NET 10 (Blazor Web App)

**Primary Dependencies**: 
- `MudBlazor` for the UI (MudFileUpload)
- `SixLabors.ImageSharp` for image conversion and compression
- `Microsoft.Extensions.Caching.Memory` for in-memory caching

**Storage**: SQL Server (Entity Framework Core)

**Testing**: N/A for this plan (Assuming manual testing in the Blazor server environment as per current workflow)

**Target Platform**: Web (Blazor Server)

**Project Type**: e-commerce web application

**Performance Goals**: Homepage loads must read from memory instantly (0 DB queries for images)

**Constraints**: In-memory cache must not cause excessive RAM pressure; max size of cached images should be bounded or we assume minimal carousel images (e.g., 5-10 images < 500KB each).

**Scale/Scope**: Homepage traffic

## Constitution Check

*GATE: Must pass before Phase 0 research. Re-check after Phase 1 design.*

No violations of project principles. Architecture aligns with existing ImageUploadService.

## Project Structure

### Documentation (this feature)

```text
specs/033-homepage-image-upload/
├── plan.md              # This file
├── research.md          # Phase 0 output 
├── data-model.md        # Phase 1 output 
└── quickstart.md        # Phase 1 output 
```

### Source Code (repository root)

```text
wowmenfashions/
├── Components/
│   └── Pages/
│       ├── AdminHomepageConfig.razor  # Modified to support MudFileUpload
│       └── Homepage.razor             # Modified to consume cached images
├── Data/
│   ├── ApplicationDbContext.cs
│   └── Entities/
│       ├── HomepageImage.cs           # New entity for storing AVIF bytes
├── Services/
│   ├── IHomepageImageService.cs       # New service interface for cache management
│   └── HomepageImageService.cs        # New service implementing cache logic
```

**Structure Decision**: Blazor Server standard architecture. We will introduce a new `HomepageImageService` which acts as a Singleton or utilizes `IMemoryCache` to hold the current active homepage images in memory.
