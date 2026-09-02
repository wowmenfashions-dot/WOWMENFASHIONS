# Implementation Tasks: Shipping Charge Threshold

## Phase 1: Setup (Shared Infrastructure)

**Purpose**: Project initialization and basic structure

- [ ] T001 Create project structure per implementation plan

---

## Phase 2: Foundational (Blocking Prerequisites)

**Purpose**: Core infrastructure that MUST be complete before ANY user story can be implemented

**CRITICAL**: No user story work can begin until this phase is complete

- [x] T002 [P] Create `StoreSettingsDto` model in `wowmenfashions/Models/StoreSettingsDto.cs`
- [x] T003 Create `StoreSettings` table and stored procedures (`dbo.StoreSettings_Get`, `dbo.StoreSettings_Update`) in the database.
- [x] T004 Implement `IStoreSettingsRepository` and `StoreSettingsRepository` in `wowmenfashions/Repositories/` (depends on T003)
- [x] T005 Implement `IStoreSettingsService` and `StoreSettingsService` in `wowmenfashions/Services/` (depends on T004)
- [x] T006 Register `StoreSettingsService` and `StoreSettingsRepository` in dependency injection (e.g., `Program.cs`)

**Checkpoint**: Foundation ready - user story implementation can now begin in parallel

---

## Phase 3: User Story 2 - Configure Shipping Threshold and Charge (Priority: P1)

**Goal**: As an admin, I want to configure the minimum threshold for free shipping and the shipping charge amount from the admin dashboard, so that I can adjust policies without code changes.

**Independent Test**: Can be tested by logging into the admin dashboard, updating the settings, and saving.

### Implementation for User Story 2

- [x] T007 [P] [US2] Create Admin UI Page `StoreSettings.razor` in `wowmenfashions/Components/Pages/Admin/StoreSettings.razor`
- [x] T008 [P] [US2] Add navigation link to `AdminNavMenu.razor` for Store Settings

**Checkpoint**: Admin configuration is independently functional.

---

## Phase 4: User Story 1 - Apply Shipping Charge to Low Value Carts (Priority: P1)

**Goal**: As a customer, when my cart total is below the minimum threshold, I am charged a shipping fee so that small orders cover their delivery costs.

**Independent Test**: Can be tested by adding items below the threshold and observing the shipping charge in the cart/checkout, then adding more items to exceed the threshold and observing the charge disappear.

### Implementation for User Story 1

- [x] T009 [P] [US1] Update `CartService.cs` (or equivalent where cart total is calculated) to fetch `StoreSettings` and apply the shipping charge.
- [x] T010 [US1] Update `wowmenfashions/Components/Pages/Cart.razor` (and any Cart Sidebar components) to display the shipping charge.
- [x] T011 [US1] Update `wowmenfashions/Components/Pages/Checkout.razor` to include the shipping charge in the final total.

**Checkpoint**: Shipping charge is fully functional on the customer side.

---

## Phase 5: Polish & Cross-Cutting Concerns

**Purpose**: Improvements that affect multiple user stories

- [x] T012 Run `quickstart.md` validation, ensuring database migrations run properly and the feature can be tested via UI.

---

## Dependencies & Execution Order

### Phase Dependencies

- **Setup (Phase 1)**: No dependencies - can start immediately
- **Foundational (Phase 2)**: Depends on Setup completion - BLOCKS all user stories
- **User Stories (Phase 3+)**: All depend on Foundational phase completion
- **Polish (Final Phase)**: Depends on all desired user stories being complete

### User Story Dependencies

- **US2 (Config)**: Depends on Foundational phase. Can be executed before or in parallel with US1.
- **US1 (Apply)**: Depends on Foundational phase. Relies on the logic and DB data populated in US2 for actual custom values, but can be tested with defaults.
