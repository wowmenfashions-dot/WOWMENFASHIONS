# Implementation Tasks: Urban Outfitters Theme Overhaul

## Phase 1: Setup
**Purpose**: Project initialization and basic structure

- [X] T001 Verify project running state and ensure CSS hot reload is active in `wowmenfashions/wwwroot/wowmenfashions.styles.css`
- [X] T002 Import `Outfit` and `Inter` Google Fonts into `App.razor` (already present, need to verify usage in theme)

---

## Phase 2: Foundational 
**Purpose**: Core infrastructure that MUST be complete before ANY user story can be implemented

- [X] T003 Update `wowmenfashions/Components/Layout/MainLayout.razor` to define the new minimalist `MudTheme` colors (Black, White, Off-white)
- [X] T004 Update `wowmenfashions/Components/Layout/MainLayout.razor` to map `MudTheme.Typography` to the new Google Fonts

**Checkpoint**: Foundation ready - user story implementation can now begin in parallel

---

## Phase 3: User Story 1 - Shopper Browsing Products (Priority: P1)
**Goal**: A customer lands on the website and sees a clean, spacious layout. Product cards are minimalist with prominent imagery, clean typography for prices and titles, and subtle hover interactions identical to Urban Outfitters.

**Independent Test**: Navigate to `/` (Homepage) and verify product cards have no shadows and images are edge-to-edge.

### Implementation for User Story 1
- [X] T005 [US1] Create CSS overrides in `wowmenfashions/wwwroot/wowmenfashions.styles.css` to remove borders and shadows from `.mud-card`
- [X] T006 [US1] Update `wowmenfashions/Components/Pages/Home.razor` to ensure product grids use the new flat minimalist card style
- [X] T007 [US1] Update `wowmenfashions/Components/Shared/ProductCard.razor` (if applicable) or inline card definitions to left-align text underneath images

**Checkpoint**: At this point, User Story 1 should be fully functional and testable independently

---

## Phase 4: User Story 2 - Customer Interaction (Priority: P2)
**Goal**: When a customer interacts with buttons or filters, they experience the same minimalist design language (e.g., sharp borders, specific font weights, stripped-back UI elements).

**Independent Test**: Hover over a button or interact with a text field; verify flat black appearance and dark gray hover state.

### Implementation for User Story 2
- [X] T008 [P] [US2] Create CSS overrides in `wowmenfashions/wwwroot/wowmenfashions.styles.css` for `.mud-button-filled` to be flat black with dark gray hover
- [X] T009 [P] [US2] Create CSS overrides in `wowmenfashions/wwwroot/wowmenfashions.styles.css` to sharpen `.mud-input` borders (remove rounded corners)

**Checkpoint**: At this point, User Stories 1 AND 2 should both work independently

---

## Phase 5: Polish & Cross-Cutting Concerns
**Purpose**: Improvements that affect multiple user stories

- [X] T010 Validate changes against `quickstart.md`
- [X] T011 Check mobile responsiveness of the new flat layout on Homepage and Checkout
- [X] T012 Code cleanup and refactoring (remove any unused old CSS classes)

---

## Dependencies & Execution Order

### Phase Dependencies
- **Setup (Phase 1)**: Can start immediately
- **Foundational (Phase 2)**: Depends on Setup completion
- **User Stories (Phase 3+)**: All depend on Foundational phase completion

### User Story Dependencies
- **User Story 1 (P1)**: Can start after Foundational (Phase 2)
- **User Story 2 (P2)**: Can start after Foundational (Phase 2) and can run in parallel with US1.
