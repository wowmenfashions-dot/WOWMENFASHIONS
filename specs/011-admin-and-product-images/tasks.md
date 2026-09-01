# Tasks: Admin Panel, Multiple Images, & Pagination

## Phase 1: Setup

- (No project initialization required, using existing structure)

---

## Phase 2: Foundational

**Purpose**: Core infrastructure and database schema updates.

- [X] T001 Create `Database/03_AdminAndImages.sql` to add `AdminUsers` and `ProductImages` tables and seed data.
- [X] T002 Update `wowmenfashions/Models/ProductDto.cs` to include `List<string> AdditionalImages`.
- [X] T003 Create `wowmenfashions/Models/AdminUserDto.cs`.

**Checkpoint**: Foundation ready.

---

## Phase 3: User Story 1 - Secure Admin Login (Priority: P1)

**Goal**: Admins need a secure way to log in.

- [X] T004 [P] [US1] Create `wowmenfashions/Services/IAuthService.cs` and `AuthService.cs` for validating credentials.
- [X] T005 [US1] Configure Cookie Authentication and register `AuthService` in `wowmenfashions/Program.cs`.
- [X] T006 [US1] Wrap `RouteView` with `AuthorizeRouteView` in `wowmenfashions/Components/Routes.razor`.
- [X] T007 [P] [US1] Create login page at `wowmenfashions/Components/Pages/Admin/Login.razor`.
- [X] T008 [US1] Create basic protected admin dashboard at `wowmenfashions/Components/Pages/Admin/Dashboard.razor`.

**Checkpoint**: Admin login and basic dashboard structure is functional.

---

## Phase 4: User Story 2 - Admin Product Management (Priority: P1)

**Goal**: Admins need a dashboard to view, add, edit, and delete products.

- [X] T009 [US2] Add product CRUD methods in `wowmenfashions/Services/IProductService.cs` and `ProductService.cs`.
- [X] T010 [US2] Update `wowmenfashions/Components/Pages/Admin/Dashboard.razor` to include a `MudDataGrid` for products.

**Checkpoint**: Admin can manage products.

---

## Phase 5: User Story 3 - Multiple Product Images Display (Priority: P2)

**Goal**: Customers should see all images associated with a product on the details page.

- [X] T011 [US3] Update `wowmenfashions/Components/Pages/ProductDetails.razor` to include a `MudCarousel` for `ImageUrl` and `AdditionalImages`.

**Checkpoint**: Product details page displays multiple images.

---

## Phase 6: User Story 4 - Category Page Pagination (Priority: P2)

**Goal**: Paginated results on category pages.

- [X] T012 [US4] Update `GetProductsAsync` signature in `wowmenfashions/Services/IProductService.cs` and `ProductService.cs` to handle pagination (`page` and `pageSize`).
- [X] T013 [US4] Update `wowmenfashions/Components/Pages/Category.razor` to include `MudPagination` and handle page state.

**Checkpoint**: Categories are paginated.

---

## Phase 7: User Story 5 - Rebranding (Priority: P3)

**Goal**: Change the application title to "WOWMEN".

- [X] T014 [US5] Change title to "WOWMEN" in `wowmenfashions/Components/Layout/MainLayout.razor`.
- [X] T015 [US5] Change title to "WOWMEN" in `wowmenfashions/Components/App.razor`.

**Checkpoint**: Application rebranded.

---

## Phase 8: Polish & Cross-Cutting Concerns

- [X] T016 Run `quickstart.md` validation tests.

---

## Dependencies & Execution Order

- Foundational tasks (T001-T003) must be executed first to support the new database tables and models.
- US1 (T004-T008) sets up the authentication required for US2.
- US2 (T009-T010) adds the product management UI.
- US3, US4, and US5 can be executed in any order, but depend on the foundational models and database being updated.
