# Research: Product Filtering & Details

## 1. Professional Product Cards
**Decision**: Use `MudCard` with custom scoped CSS for `object-fit: cover` and hover micro-animations.
**Rationale**: MudBlazor doesn't provide built-in hover elevation transitions by default on cards. Custom CSS in `ProductCard.razor.css` ensures images don't distort while keeping the professional MudBlazor layout. 

## 2. Cascading Filter Dropdowns
**Decision**: Use `CategoryDto` with a `ParentId` property. In `Category.razor`, clicking a parent category will trigger an asynchronous fetch for its subcategories and populate a secondary filter list or `MudSelect`.
**Rationale**: This keeps the UI responsive and cleanly separates the primary category routing (`/category/mens`) from subcategory filtering (state changes on the same page).

## 3. Product Details Page
**Decision**: Create a new routable component `@page "/product/{Id:int}"`.
**Rationale**: Standard Blazor routing practice. It will use the `Id` to query `IProductService.GetProductByIdAsync(Id)` and display detailed descriptions and a larger `MudImage`.
