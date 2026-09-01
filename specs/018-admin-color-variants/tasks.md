# Tasks: Admin Product Color Variants

## Phase 1: Setup

*(No specific setup tasks required)*

## Phase 2: Foundational (Blocking Prerequisites)

- [x] T001 Update `wowmenfashions/Data/Entities/Product.cs` to add the `ColorImages` navigation property.
- [x] T002 Create `wowmenfashions/Data/Entities/ProductColorImage.cs` entity class.
- [x] T003 Update `wowmenfashions/Data/AppDbContext.cs` to add `DbSet<ProductColorImage>` and configure relationships if needed.
- [x] T004 Run EF Core migrations to update the SQLite database schema.

## Phase 3: User Story 1 - Admin Image Upload for Colors (Priority: P1)

- [x] T005 [US1] Update `wowmenfashions/Services/ProductService.cs` and `ProductDto.cs` (if needed) to map and return the real `ColorImages` from the database instead of the hardcoded logic.
- [x] T006 [US1] Update `wowmenfashions/Components/Pages/AdminProducts.razor` to include a UI section where admins can upload or assign images to specific colors when editing a product.
- [x] T007 [US1] Verify `wowmenfashions/Components/Pages/ProductDetails.razor` correctly switches the main image using the dynamic `ColorImages` property.

## Phase 4: Polish & Cross-Cutting Concerns

- [x] T008 Restart application and test end-to-end functionality via `quickstart.md` validation scenarios.
