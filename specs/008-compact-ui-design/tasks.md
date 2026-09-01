# Implementation Tasks: Compact UI Redesign

**Feature**: `specs/008-compact-ui-design/spec.md`

## Phase 1: Setup (Shared Infrastructure)

**Purpose**: Project initialization and basic structure
*No specific setup tasks for this feature.*

---

## Phase 2: Foundational (Blocking Prerequisites)

**Purpose**: Core infrastructure that MUST be complete before ANY user story can be implemented
*No specific foundational tasks for this feature.*

---

## Phase 3: User Story 1 - Compact Global Layout (Priority: P1) 🚀 MVP

**Goal**: Shrink components, spacing, and grids globally to fit more on a single screen without scrolling.

**Independent Test**: Navigate to the category page. The product grid should be packed tightly (`Spacing="1"`), all filters should be dense, and margins should be noticeably smaller.

### Implementation for User Story 1

- [x] T001 [P] [US1] Update `wowmenfashions/Components/Layout/MainLayout.razor` and `NavMenu.razor` to reduce Appbar height and nav link margins.
- [x] T002 [P] [US1] Update `wowmenfashions/Components/Pages/Category.razor` to use `Dense="true"`, `Margin="Margin.Dense"`, and `Size="Size.Small"` for all inputs, and reduce grid spacing.
- [x] T003 [P] [US1] Update `wowmenfashions/Components/Shared/ProductCard.razor` to reduce padding around the image and condense the text/price area.
- [x] T004 [P] [US1] Update `wowmenfashions/Components/Pages/ProductDetails.razor` to shrink the max-height of the hero image and reduce text margins.
- [x] T005 [P] [US1] Update `wowmenfashions/Components/Pages/Cart.razor` to apply `Dense="true"` to the `MudTable` and compact the summary box.

**Checkpoint**: At this point, the entire application should look visibly denser and more compact on desktop and mobile.

---

## Phase 4: Polish & Cross-Cutting Concerns

**Purpose**: Improvements that affect multiple user stories

- [ ] T006 Run quickstart.md validation across the main views (Category, Details, Cart) to ensure scrolling is minimized while keeping the app visually appealing.

---

## Dependencies & Execution Order

### Phase Dependencies

- **User Stories (Phase 3)**: Can run immediately.
- **Polish (Final Phase)**: Depends on all desired user stories being complete.

### Parallel Opportunities

- T001 through T005 touch completely different `.razor` files and can all be executed entirely in parallel!
