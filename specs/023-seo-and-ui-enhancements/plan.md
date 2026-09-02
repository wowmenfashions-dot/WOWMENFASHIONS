# Implementation Plan: SEO and UI Enhancements

**Branch**: `[023-seo-and-ui-enhancements]` | **Date**: 2026-09-01 | **Spec**: [spec.md](./spec.md)

**Input**: Feature specification from `/specs/023-seo-and-ui-enhancements/spec.md`

## Summary

This feature implements vital SEO improvements, including dynamic `sitemap.xml` and `robots.txt` generation, and integrates meta tags using Blazor's `<HeadContent>`. Additionally, it updates the application logo, sets the favicon, and enhances the UI by correctly aligning the Cart Badge and adding trust banners (Guarantee, Delivery, YKK) directly below the homepage carousel.

## Technical Context

**Language/Version**: C# / .NET 10 (Blazor Server)
**Primary Dependencies**: MudBlazor
**Storage**: SQL Server (Dapper) - not directly impacted by UI changes, but used for dynamic sitemap.
**Testing**: Unit/Integration Tests
**Target Platform**: Web (Modern Browsers, Corporate network friendly)
**Project Type**: Blazor Server Web Application (Modular Monolith)
**Performance Goals**: Fast Time-to-First-Byte for SEO crawlers.
**Constraints**: Standard HTTP/HTTPS standard ports, Long Polling fallback for WebSockets.
**Scale/Scope**: Homepage UI updates and app-wide SEO tags.

## Constitution Check

*GATE: Must pass before Phase 0 research. Re-check after Phase 1 design.*

- **Principle 1-21 (Database/Dapper)**: Not directly applicable to UI/SEO changes, except that any dynamic sitemap generation MUST use existing Repositories/Services and not direct SQL.
- **Principle 23 (Tech Stack)**: Follows Blazor Server, MudBlazor, C#.
- **Principle 28 (Warnings)**: Code changes will not introduce warnings.
- **Principle 29 (AVIF)**: We will ensure the downloaded logo is converted/served optimally (or kept as provided for favicon).

## Project Structure

### Documentation (this feature)

```text
specs/023-seo-and-ui-enhancements/
├── plan.md              # This file
├── research.md          # SEO & Blazor best practices
├── data-model.md        # No new entities, updates to Sitemap generation
└── quickstart.md        # Guide to verifying SEO and UI
```

### Source Code (repository root)

```text
wowmenfashions/
├── wwwroot/
│   ├── logo.png
│   ├── favicon.ico
│   └── robots.txt
├── Controllers/
│   └── SitemapController.cs (for dynamic sitemap.xml)
├── Components/
│   ├── App.razor
│   ├── Layout/MainLayout.razor
│   ├── Pages/Home.razor
│   └── Shared/CartWidget.razor
```

**Structure Decision**: Standard ASP.NET Core Blazor Server layout. A new Controller for `sitemap.xml` is added.
