# Implementation Plan: [FEATURE]

**Branch**: `[###-feature-name]` | **Date**: [DATE] | **Spec**: [link]

**Input**: Feature specification from `/specs/[###-feature-name]/spec.md`

**Note**: This template is filled in by the `/speckit-plan` command; its definition describes the execution workflow.

## Summary

# Implementation Plan: Admin Panel, Multiple Images, & Pagination

## Technical Context

This feature introduces authentication, administrative capabilities, image galleries, and pagination.

- **Frontend Framework**: Blazor Server with MudBlazor
- **Authentication**: ASP.NET Core built-in Cookie Authentication scheme with `Microsoft.AspNetCore.Components.Authorization`.
- **Database**: SQL Server. We will add `AdminUsers` and `ProductImages` tables.
- **Product Gallery**: `ProductDetails.razor` will use a MudCarousel or MudGrid to display multiple images.
- **Pagination**: `Category.razor` will use `MudPagination` and the `ProductService` will be updated to accept `page` and `pageSize` parameters (OFFSET/FETCH in SQL).

## Constitution Check

- **Principle 29**: AVIF Image Format Only. (We must ensure any seeded fallback or placeholder gallery images use AVIF).
- **Principle 17**: Responsive Design (Mobile First). The admin panel and product gallery must be mobile-friendly.

## Phase 0: Research (If needed)
- **Authentication Strategy**: Cookie-based authentication is natively supported in Blazor Server and easy to set up without heavy Identity frameworks. We will use `CookieAuthenticationDefaults.AuthenticationScheme`.

## Phase 1: Design & Contracts

- [x] `data-model.md`: Defined `AdminUsers` and `ProductImages` tables.
- [x] `quickstart.md`: Test validation guide created.

## Proposed Changes

### Rebranding
- [MODIFY] `wowmenfashions/Components/Layout/MainLayout.razor` (Change "Application" to "WOWMEN").
- [MODIFY] `wowmenfashions/Components/App.razor` (Change `<title>` to "WOWMEN").

### Database & Models
- [NEW] `Database/03_AdminAndImages.sql` (Creates `AdminUsers` and `ProductImages` tables, inserts default admin, and adds dummy images to products).
- [MODIFY] `wowmenfashions/Models/ProductDto.cs` (Add `List<string> AdditionalImages`).
- [NEW] `wowmenfashions/Models/AdminUserDto.cs` (Admin model).

### Services
- [MODIFY] `wowmenfashions/Services/IProductService.cs` (Add pagination parameters, return total count, and load additional images).
- [MODIFY] `wowmenfashions/Services/ProductService.cs` (Implement OFFSET/FETCH for pagination. Join or query `ProductImages`).
- [NEW] `wowmenfashions/Services/IAuthService.cs` and `AuthService.cs` (Validate admin credentials against database).

### Authentication Setup
- [MODIFY] `wowmenfashions/Program.cs` (Add Cookie Authentication and CascadingAuthenticationState).
- [MODIFY] `wowmenfashions/Components/Routes.razor` (Wrap `RouteView` in `AuthorizeRouteView`).
- [NEW] `wowmenfashions/Components/Pages/Admin/Login.razor` (Admin login form).

### Admin Dashboard
- [NEW] `wowmenfashions/Components/Pages/Admin/Dashboard.razor` (Requires `[Authorize]`. Displays a `MudDataGrid` of products with CRUD actions).

### UI Updates
- [MODIFY] `wowmenfashions/Components/Pages/Category.razor` (Add `MudPagination`, handle page state, and pass to service).
- [MODIFY] `wowmenfashions/Components/Pages/ProductDetails.razor` (Add MudCarousel for `Product.ImageUrl` + `Product.AdditionalImages`).

## User Review Required

> [!IMPORTANT]
> **Admin Authentication**: We are using simple Cookie-based authentication with a custom `Admins` table instead of the full ASP.NET Core Identity framework. This is lightweight, secure, and perfect for an MVP admin panel. The default login will be `admin` / `password`.

## Verification Plan

### Manual Verification
1. Verify tab title says WOWMEN.
2. Visit `/category/mens` and verify pagination controls exist and work.
3. Visit `/admin` and verify redirection to `/login`.
4. Log in with `admin`/`password` and verify access to the dashboard.
5. Visit a product page and verify the image carousel works.

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
