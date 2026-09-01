# Implementation Plan: admin-product-creation

**Feature Branch**: `[021-admin-product-creation]`

## User Review Required

> [!IMPORTANT]
> The admin page will be added to the Blazor application at the route `/admin/products/new`.
> We will create a new Blazor component `AdminProductCreate.razor` under `wowmenfashions/Components/Pages/Admin`.
> Wait, is there already an admin area? If not, we will just create this page and protect it with `@attribute [Authorize(Roles = "Admin")]` or similar, depending on existing auth.

## Proposed Changes

### Database Layer
#### [MODIFY] [ProductsRepository.cs](file:///c:/MyDrive/ProjectDrive/WOWMENFASHIONS/wowmenfashions/Data/ProductsRepository.cs)
- Add `CreateProductAsync` method that calls a new stored procedure `Product_Create`.

#### [NEW] [Product_Create.sql](file:///c:/MyDrive/ProjectDrive/WOWMENFASHIONS/Product_Create.sql)
- Creates the product and returns the new Product Id.

#### [NEW] [ProductColorImage_Create.sql](file:///c:/MyDrive/ProjectDrive/WOWMENFASHIONS/ProductColorImage_Create.sql)
- Inserts records into `ProductColorImages`.

### Blazor Components
#### [NEW] [AdminProductCreate.razor](file:///c:/MyDrive/ProjectDrive/WOWMENFASHIONS/wowmenfashions/Components/Pages/AdminProductCreate.razor)
- A MudBlazor form for Product Name, Description, Price, Original Price, Category, and a dynamic list of colors and images.

### Services
#### [MODIFY] [ProductService.cs](file:///c:/MyDrive/ProjectDrive/WOWMENFASHIONS/wowmenfashions/Services/ProductService.cs)
- Add `CreateProductAsync` method.

## Verification Plan

### Automated Tests
- N/A

### Manual Verification
- Navigate to `/admin/products/new`.
- Ensure it requires login/admin rights.
- Fill out form and submit.
- Verify product appears in the database and catalog.
