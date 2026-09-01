# Implementation Tasks: State, Caching, and Cart

**Feature**: `specs/006-state-caching-cart/spec.md`

## Phase 1: Setup (Shared Infrastructure)

**Purpose**: Project initialization and basic structure

- [x] T001 Ensure `AddMemoryCache()` is registered in `wowmenfashions/Program.cs` (if not already present).

---

## Phase 2: Foundational (Blocking Prerequisites)

**Purpose**: Core infrastructure that MUST be complete before ANY user story can be implemented
*No specific foundational tasks for this feature.*

---

## Phase 3: User Story 2 - In-Memory Caching (Priority: P1) 🚀 MVP

**Goal**: Fetch all reference data from SQL but cache it in-memory.

**Independent Test**: Load categories, then check logs to ensure subsequent loads do not hit the database.

### Implementation for User Story 2

- [x] T002 [US2] Update `wowmenfashions/Services/ProductService.cs` to inject `IMemoryCache` and implement caching for category and product fetches.

**Checkpoint**: At this point, the application should load data from memory after the initial database hit.

---

## Phase 4: User Story 1 - Clear Filters on Navigation (Priority: P1)

**Goal**: Reset previous filters when switching main categories.

**Independent Test**: Set a filter in one category, switch to another, and verify the filter resets.

### Implementation for User Story 1

- [x] T003 [US1] Update `wowmenfashions/Components/Pages/Category.razor`'s `OnParametersSetAsync` to detect when `Slug` changes and reset all filter variables (`_maxPrice`, `_selectedColor`, etc.).

**Checkpoint**: At this point, navigation between categories should offer a clean slate.

---

## Phase 5: User Story 3 - Visible and Functional Cart Page (Priority: P1)

**Goal**: Create a routable `/cart` page to view added items.

**Independent Test**: Click the shopping cart icon and verify a cart interface appears instead of a 404.

### Implementation for User Story 3

- [x] T004 [US3] Create `wowmenfashions/Components/Pages/Cart.razor` implementing a mobile-first `MudGrid` or `MudTable` to display cart items and total price.

**Checkpoint**: All user stories should now be independently functional.

---

## Phase 6: Polish & Cross-Cutting Concerns

**Purpose**: Improvements that affect multiple user stories

- [x] T005 Run quickstart.md validation across all three scenarios (caching, filter reset, cart).

---

## Dependencies & Execution Order

### Phase Dependencies

- **Setup (Phase 1)**: Must run first.
- **User Stories (Phase 3+)**: Depend on Setup completion. Can run in parallel.
- **Polish (Final Phase)**: Depends on all desired user stories being complete.

### Parallel Opportunities

- T002 (ProductService), T003 (Category.razor), and T004 (Cart.razor) modify completely different files and can be executed independently/in parallel.
