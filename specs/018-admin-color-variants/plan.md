# Implementation Plan: Admin Product Color Variants Management

**Branch**: `018-admin-color-variants` | **Date**: 2026-08-31 | **Spec**: [spec.md](spec.md)

**Input**: Feature specification from `specs/018-admin-color-variants/spec.md`

## Summary

We will update the database schema to include a `ProductColorImage` mapping entity to allow products to define specific images per color variant. We will then update the `AdminProducts.razor` UI to allow administrators to upload and manage these variant images. Finally, we will refactor `ProductService` to read these values dynamically instead of using hardcoded mock data.

## Technical Context

**Language/Version**: C#, Blazor Server, EF Core
**Primary Dependencies**: Microsoft.EntityFrameworkCore
**Storage**: SQLite (app.db)
**Testing**: Manual UI validation
**Target Platform**: Web
**Project Type**: Blazor Web App

## Constitution Check

No violations. Enhancing data models and administrative control aligns with standard best practices.

## Project Structure

### Documentation (this feature)

```text
specs/018-admin-color-variants/
├── plan.md
├── research.md
├── data-model.md
├── quickstart.md
└── tasks.md
```

### Source Code

```text
wowmenfashions/
├── Data/
│   ├── Entities/
│   │   ├── Product.cs
│   │   └── ProductColorImage.cs [NEW]
│   └── AppDbContext.cs
├── Migrations/
│   └── [NEW EF Core Migration]
├── Services/
│   └── ProductService.cs
├── Components/
│   └── Pages/
│       ├── AdminProducts.razor
│       └── ProductDetails.razor
```

## Complexity Tracking

Moderate backend complexity (new EF core entity and migration). High UI complexity (updating the admin dashboard to manage a collection of color-image mappings for a product).
