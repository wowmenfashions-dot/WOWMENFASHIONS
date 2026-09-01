# Tasks: UI Theme & Layout Enhancements

## Phase 1: Setup
- (No setup required for UI enhancements)

---

## Phase 2: Foundational
- (No foundational infrastructure required)

---

## Phase 3: User Story 1 - Apply Global Theme & Colors (Priority: P1)

**Goal**: Users should see a cohesive, premium color theme applied across the entire web application.

**Independent Test**: Load the application and verify the new theme colors.

### Implementation for User Story 1

- [X] T001 [P] [US1] Update `_lightPalette` and `_darkPalette` to premium theme colors in `wowmenfashions/Components/Layout/MainLayout.razor`

**Checkpoint**: At this point, User Story 1 should be fully functional and testable independently

---

## Phase 4: User Story 2 - Fix Header, Layout, and Filter Spacing (Priority: P1)

**Goal**: Users browsing categories should have a compact top header, properly aligned filters, and visible cart item counts.

**Independent Test**: Navigate to Category page, verify minimal top whitespace, single-row filters, and check cart badge.

### Implementation for User Story 2

- [X] T002 [US2] Reduce top padding on `MudMainContent` in `wowmenfashions/Components/Layout/MainLayout.razor`
- [X] T003 [P] [US2] Set `Overlap="false"` or add `mr-2` to `MudBadge` in `wowmenfashions/Components/Shared/CartWidget.razor`
- [X] T004 [P] [US2] Remove excess top margin/padding and align filter grid to a single row in `wowmenfashions/Components/Pages/Category.razor`

**Checkpoint**: At this point, User Stories 1 AND 2 should both work independently

---

## Phase 5: User Story 3 - Polish Product Card Layout (Priority: P2)

**Goal**: Users viewing product cards should see spacing between adjacent cards, sale badges positioned correctly, and a default placeholder image.

**Independent Test**: View product grid, verify spacing, verify sale badge, and verify image fallback.

### Implementation for User Story 3

- [X] T005 [US3] Add `Spacing="3"` to product `MudGrid` in `wowmenfashions/Components/Pages/Category.razor`
- [X] T006 [P] [US3] Fix sale badge positioning and add image fallback in `wowmenfashions/Components/Shared/ProductCard.razor`

**Checkpoint**: All user stories should now be independently functional

---

## Phase 6: Polish & Cross-Cutting Concerns

**Purpose**: Improvements that affect multiple user stories

- [X] T007 Run quickstart.md validation tests to ensure all UI elements look perfect.

---

## Dependencies & Execution Order

### Phase Dependencies
- **User Stories (Phase 3+)**: Can proceed in sequence. T001 and T002 modify the same file (`MainLayout.razor`), so they must be done sequentially. T004 and T005 modify `Category.razor` and must be sequential.

### Parallel Opportunities
- T003 and T004 can be executed in parallel (different files).
- T006 can be executed in parallel with T005.
