# Implementation Plan: [FEATURE]

**Branch**: `[###-feature-name]` | **Date**: [DATE] | **Spec**: [link]

**Input**: Feature specification from `/specs/[###-feature-name]/spec.md`

**Note**: This template is filled in by the `/speckit-plan` command; its definition describes the execution workflow.

## Summary

This feature involves updating the MudBlazor components in the UI layer of the application. The goal is to apply a global cohesive color theme, optimize layout spacing, align filters, and improve the product grid presentation.

## Technical Context

This feature involves updating the MudBlazor components in the UI layer of the application. The goal is to apply a global cohesive color theme, optimize layout spacing, align filters, and improve the product grid presentation.

- **Frontend Framework**: Blazor Server with MudBlazor
- **Styling**: `MainLayout.razor` (Theme configuration, Appbar), `Category.razor` (Layout, filter grid), `ProductCard.razor` (Card layout, image rendering, sale badge).
- **Cart Badge**: `CartWidget.razor` using `MudBadge`.

## Constitution Check

- **Principle 15**: Keep logic out of razor files. (We will only be updating UI logic and CSS classes).
- **Principle 17**: Responsive Design (Mobile First). We must ensure the filters and layout work well on small screens.
- **Principle 29**: AVIF Image Format Only. (Fallback image must be AVIF or standard SVG).

## Proposed Changes

### Global Theme (wowmenfashions/Components/Layout/MainLayout.razor)
- [MODIFY] `wowmenfashions/Components/Layout/MainLayout.razor`
  - Update `_lightPalette` and `_darkPalette` to a premium color scheme (e.g., Deep Navy primary `#1A237E`, Gold Accent `#FFC107`, crisp background `#F8F9FA`).
  - Add specific typography settings if needed.
  - The `pt-12 pa-2` on `MudMainContent` is contributing to the "too much space" on the category page. Change `pt-12` to a smaller value or manage spacing within the page.

### Cart Badge (wowmenfashions/Components/Shared/CartWidget.razor)
- [MODIFY] `wowmenfashions/Components/Shared/CartWidget.razor`
  - The `MudBadge` overlaps too aggressively. Set `Overlap="false"` or add `Class="mr-2"` to ensure the number is fully visible, especially for numbers > 9.

### Category Filters & Spacing (wowmenfashions/Components/Pages/Category.razor)
- [MODIFY] `wowmenfashions/Components/Pages/Category.razor`
  - Remove `mt-2` and `mb-4` from `MudContainer` and use `pt-2` to eliminate excess top space.
  - Update the `MudGrid` for filters. Currently it uses `xs="12" sm="6" md="3"`. Change it to force a single row on desktop (`md="auto"`) or adjust widths to ensure they fit nicely. Use `Class="d-flex align-end"` or similar flexbox properties to keep them in one line.
  - Add `Spacing="3"` to the product `MudGrid` so the cards have spacing between them.

### Product Card Polish (wowmenfashions/Components/Shared/ProductCard.razor)
- [MODIFY] `wowmenfashions/Components/Shared/ProductCard.razor`
  - Ensure the `sale-badge` is positioned correctly (it's currently `position-absolute top-0 start-0`).
  - Add an image fallback mechanism. Since MudImage doesn't natively support a robust fallback without JS, we can use a standard `<img>` tag with `onerror="this.src='/images/placeholder.svg';"` or handle it via a boolean flag in Blazor if possible. Alternatively, a simple `object-fit: cover; background-color: #f5f5f5;` acts as a placeholder if the image fails.

## Validation Plan

### Automated Tests
- N/A for UI visual changes. Run `dotnet build` to ensure no syntax errors.

### Manual Verification
1. Open the application and verify the new color theme is applied.
2. View the Cart icon to ensure the badge number is not cut off.
3. Navigate to a Category page (e.g., Mens).
4. Verify the top whitespace is reduced.
5. Verify the filters fit cleanly in a single row on desktop.
6. Verify the product cards have spacing between them.
7. Verify the Sale badge overlaps the image correctly.
8. Modify a product in the database to have an invalid ImageUrl and verify the fallback renders correctly.

## Project Structure

### Documentation (this feature)

```text
specs/[###-feature]/
├── plan.md              # This file (/speckit-plan command output)
├── research.md          # Phase 0 output (/speckit-plan command)
├── data-model.md        # Phase 1 output (/speckit-plan command)
├── quickstart.md        # Phase 1 output (/speckit-plan command)
├── contracts/           # Phase 1 output (/speckit-plan command)
└── tasks.md             # Phase 2 output (/speckit-tasks command - NOT created by /speckit-plan)
```

### Source Code (repository root)

```text
# Option 2: Web application (when "frontend" + "backend" detected)
backend/
├── src/
│   ├── models/
│   ├── services/
│   └── api/
└── tests/

frontend/
├── src/
│   ├── components/
│   ├── pages/
│   └── services/
└── tests/

# [REMOVE IF UNUSED] Option 3: Mobile + API (when "iOS/Android" detected)
api/
└── [same as backend above]

ios/ or android/
└── [platform-specific structure: feature modules, UI flows, platform tests]
```

**Structure Decision**: [Document the selected structure and reference the real
directories captured above]

## Complexity Tracking

> **Fill ONLY if Constitution Check has violations that must be justified**

| Violation | Why Needed | Simpler Alternative Rejected Because |
|-----------|------------|-------------------------------------|
| [e.g., 4th project] | [current need] | [why 3 projects insufficient] |
| [e.g., Repository pattern] | [specific problem] | [why direct DB access insufficient] |
