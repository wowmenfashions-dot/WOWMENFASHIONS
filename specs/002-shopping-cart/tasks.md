# Implementation Tasks: Stage 2 - Shopping Cart & Guest Cart

## Phase 1: Setup (Shared Infrastructure)

**Purpose**: Project initialization and basic structure.

- [x] T001 Verify and add the SQL Server connection string to `wowmenfashions/appsettings.json`
- [x] T002 Configure `wowmenfashions/Program.cs` to map configuration to the Dapper context or DB connection factory

---

## Phase 2: Foundational (Blocking Prerequisites)

**Purpose**: Core infrastructure that MUST be complete before ANY user story can be implemented.

- [x] T003 [P] Create `Cart` entity model in `wowmenfashions/Data/Entities/Cart.cs`
- [x] T004 [P] Create `CartItem` entity model in `wowmenfashions/Data/Entities/CartItem.cs`
- [x] T005 [P] Define `ICartRepository` interface in `wowmenfashions/Data/ICartRepository.cs`
- [x] T006 [P] Define `ICartService` interface in `wowmenfashions/Services/ICartService.cs`
- [x] T007 Register `ICartRepository` and `ICartService` with Dependency Injection in `wowmenfashions/Program.cs`
- [x] T008 Create `Cart` and `CartItem` table creation scripts in `wowmenfashions/Database/Tables/002_CartTables.sql`

**Checkpoint**: Foundation ready - user story implementation can now begin.

---

## Phase 3: User Story 1 - Add Product to Cart (Priority: P1) ⭐ MVP

**Goal**: Allow guest users to add items to their cart.
**Independent Test**: User can click "Add to Cart" and the cart item count increases.

### Implementation for User Story 1

- [x] T009 [US1] Create SQL stored procedure in `wowmenfashions/Database/StoredProcedures/dbo.Cart_AddItem.sql`
- [x] T010 [US1] Implement `AddItemAsync` method in `wowmenfashions/Data/CartRepository.cs` using Dapper
- [x] T011 [US1] Implement `AddItemAsync` business logic in `wowmenfashions/Services/CartService.cs`
- [x] T012 [US1] Integrate `CartService.AddItemAsync` into the Product Details UI component (e.g., `wowmenfashions/Components/Pages/Products.razor` or similar)

**Checkpoint**: User Story 1 is fully functional and testable independently.

---

## Phase 4: User Story 2 - View and Manage Cart (Priority: P1)

**Goal**: View shopping cart and modify quantities or remove items.
**Independent Test**: Navigate to `/cart` to view items, change quantities, and remove items successfully.

### Implementation for User Story 2

- [x] T013 [P] [US2] Create SQL stored procedure `wowmenfashions/Database/StoredProcedures/dbo.Cart_Get.sql`
- [x] T014 [P] [US2] Create SQL stored procedure `wowmenfashions/Database/StoredProcedures/dbo.Cart_UpdateItem.sql`
- [x] T015 [P] [US2] Create SQL stored procedure `wowmenfashions/Database/StoredProcedures/dbo.Cart_RemoveItem.sql`
- [x] T016 [US2] Implement Get, Update, Remove methods in `wowmenfashions/Data/CartRepository.cs` using Dapper
- [x] T017 [US2] Implement Get, Update, Remove logic in `wowmenfashions/Services/CartService.cs`
- [x] T018 [US2] Create Blazor page `wowmenfashions/Components/Pages/Cart.razor` with item listing and modification controls
- [x] T019 [US2] Create Blazor component `wowmenfashions/Components/Shared/CartWidget.razor` for navigation item count

**Checkpoint**: User Stories 1 AND 2 should both work independently.

---

## Phase 5: User Story 3 - Cart Persistence across Sessions (Priority: P2)

**Goal**: Cart contents persist across page reloads via cookie.
**Independent Test**: Add items, reload page, verify items remain.

### Implementation for User Story 3

- [x] T020 [US3] Create `wowmenfashions/Services/GuestSessionService.cs` to generate/read `GuestCartId` from HTTP-Only Cookies
- [x] T021 [US3] Register `GuestSessionService` in `wowmenfashions/Program.cs`
- [x] T022 [US3] Integrate `GuestSessionService` into `CartService.cs` so all calls automatically use the persistent guest ID

**Checkpoint**: Guest carts now persist reliably across browsing sessions.

---

## Phase 6: User Story 4 - Checkout Authentication Enforcement (Priority: P1)

**Goal**: Force login/registration when proceeding to checkout.
**Independent Test**: Clicking "Proceed to Checkout" from cart redirects to `/login`.

### Implementation for User Story 4

- [x] T023 [US4] Add "Proceed to Checkout" button logic to `wowmenfashions/Components/Pages/Cart.razor`
- [x] T024 [US4] Implement redirection to `/login` if `AuthenticationStateProvider` indicates the user is unauthenticated

**Checkpoint**: All user stories should now be independently functional.

---

## Phase N: Polish & Cross-Cutting Concerns

**Purpose**: Improvements that affect multiple user stories.

- [x] T025 Execute all generated SQL scripts against the local SQL Server database
- [x] T026 Run the `quickstart.md` validation scenarios manually to confirm complete functionality

---

## Dependencies & Execution Order

### Phase Dependencies
- **Setup (Phase 1)**: No dependencies - can start immediately.
- **Foundational (Phase 2)**: Depends on Setup completion - BLOCKS all user stories.
- **User Stories (Phases 3-6)**: Depend on Foundational phase. Can be implemented sequentially.
- **Polish (Final Phase)**: Depends on all user stories being complete.

### Parallel Opportunities
- Foundational tasks (T003-T006) can be created in parallel.
- SQL Stored Procedures for US2 (T013-T015) can be created in parallel.
