# Tasks: admin-product-creation

## Phase 1: Setup (Shared Infrastructure)

**Purpose**: Project initialization and basic structure

- [X] T001 [P] Create `Product_Create.sql` script for creating new products in database
- [X] T002 [P] Create `ProductColorImage_Create.sql` script for inserting product color variants

---

## Phase 2: Foundational (Blocking Prerequisites)

**Purpose**: Core infrastructure that MUST be complete before ANY user story can be implemented

**🚫 CRITICAL**: No user story work can begin until this phase is complete

- [X] T003 Execute `Product_Create.sql` against the database
- [X] T004 Execute `ProductColorImage_Create.sql` against the database
- [X] T005 [P] Add `CreateProductAsync` method to `IProductRepository` in `wowmenfashions/Data/IProductRepository.cs`
- [X] T006 [P] Add `CreateProductAsync` method to `IProductService` in `wowmenfashions/Services/IProductService.cs`

**Checkpoint**: Foundation ready - user story implementation can now begin in parallel

---

## Phase 3: User Story 1 - Admin Access (Priority: P1) ⭐ MVP

**Goal**: As an administrator, I want a secure page accessible only to me.

**Independent Test**: Try to access the admin page as an unauthenticated user or regular customer, and verify access is denied. Then login as admin and verify access is granted.

### Implementation for User Story 1

- [X] T007 [P] [US1] Create the empty `AdminProductCreate.razor` page in `wowmenfashions/Components/Pages/AdminProductCreate.razor`
- [X] T008 [US1] Add `[Authorize]` attributes or role checks to `AdminProductCreate.razor` to restrict access to Admins

**Checkpoint**: At this point, User Story 1 should be fully functional and testable independently

---

## Phase 4: User Story 2 - Create New Product (Priority: P1)

**Goal**: As an administrator, I want to fill out a form with product details so that I can add a new product.

**Independent Test**: Admin fills out the form with valid data, submits it, and the new product appears in the store's product list.

### Implementation for User Story 2

- [X] T009 [P] [US2] Implement `CreateProductAsync` in `wowmenfashions/Data/ProductsRepository.cs` calling the new `Product_Create` SP
- [X] T010 [US2] Implement `CreateProductAsync` in `wowmenfashions/Services/ProductService.cs`
- [X] T011 [US2] Add the MudBlazor form to `wowmenfashions/Components/Pages/AdminProductCreate.razor` for base fields (Name, Price, Category, etc.)
- [X] T012 [US2] Wire the form submit button to call `ProductService.CreateProductAsync`

**Checkpoint**: At this point, User Stories 1 AND 2 should both work independently

---

## Phase 5: User Story 3 - Add Product Colors and Images (Priority: P2)

**Goal**: As an administrator, I want to be able to define multiple color variants and assign an image to each color.

**Independent Test**: Add a product with multiple colors and images, then verify on the product details page that switching colors updates the image.

### Implementation for User Story 3

- [X] T013 [P] [US3] Add a dynamic list interface in `wowmenfashions/Components/Pages/AdminProductCreate.razor` to add multiple colors and image URLs
- [X] T014 [US3] Update `ProductService.CreateProductAsync` to also invoke the logic to save colors using the new repository methods
- [X] T015 [US3] Test creating a product with multiple colors and verify it appears correctly on the store

**Checkpoint**: All user stories should now be independently functional

---

## Phase 6: Polish & Cross-Cutting Concerns

**Purpose**: Improvements that affect multiple user stories

- [X] T016 [P] Add navigation link for Admins in `wowmenfashions/Components/Layout/NavMenu.razor` to reach `/admin/products/new`
- [X] T017 Code cleanup and refactoring
- [X] T018 Run quickstart.md validation
