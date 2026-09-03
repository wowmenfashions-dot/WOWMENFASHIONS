# Implementation Plan: Deo Theme Overhaul

**Branch**: `031-deo-theme-overhaul` | **Date**: 2026-09-03 | **Spec**: [spec.md](file:///C:/MyDrive/ProjectDrive/WOWMENFASHIONS/specs/031-deo-theme-overhaul/spec.md)

**Input**: Feature specification from `specs/031-deo-theme-overhaul/spec.md`

## Summary

Overhaul the global theme and aesthetic of the WOWMENFASHIONS Blazor application to match the modern, soft, minimalist aesthetic of the "Deo eCommerce" template. This involves migrating to soft charcoal black colors, pill-shaped geometry for buttons/inputs, and heavily-rounded light gray backgrounds for product cards without borders or shadows.

## Technical Context

**Language/Version**: C# 10 / .NET 10 (Blazor Interactive Server)

**Primary Dependencies**: MudBlazor

**Storage**: N/A (UI only)

**Testing**: N/A (Visual UI overhaul)

**Target Platform**: Web Browsers (Responsive Desktop/Mobile)

**Project Type**: Blazor Web App

**Performance Goals**: Avoid heavy CSS filters or animations to maintain snappy render times.

**Constraints**: Must override the previous Urban Outfitters theme within `wowmenfashions.styles.css` (or `app.css`) and `MainLayout.razor` without changing the underlying MudBlazor component logic.

**Scale/Scope**: Global UI styling affecting all pages.

## Constitution Check

*GATE: Must pass before Phase 0 research. Re-check after Phase 1 design.*

No violations. The changes are strictly confined to presentation layers (CSS and MudTheme overrides) and do not leak into business logic or services.

## Project Structure

### Documentation (this feature)

```text
specs/031-deo-theme-overhaul/
├── plan.md              # This file
├── research.md          # Phase 0 output
├── data-model.md        # Phase 1 output (N/A)
├── quickstart.md        # Phase 1 output
└── contracts/           # Phase 1 output (N/A)
```

### Source Code (repository root)

```text
wowmenfashions/
├── wwwroot/
│   └── app.css (Global overrides for inputs, buttons, cards)
├── Components/
│   ├── Layout/
│   │   └── MainLayout.razor (MudTheme palette and typography config)
│   └── Shared/
│       └── ProductCard.razor (Image background and border-radius updates)
```

**Structure Decision**: Modifying existing UI components and CSS stylesheets in place. No structural changes needed.

## Complexity Tracking

N/A
