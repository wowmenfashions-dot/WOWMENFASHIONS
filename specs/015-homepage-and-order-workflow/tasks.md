# Implementation Tasks: Homepage & Order Workflow Redesign

## Phase 1: Setup (Shared Infrastructure)

**Purpose**: Project initialization and basic structure

- [x] T001 Setup image assets by saving the provided RFID and Zip Wallet banner images as AVIF files into `wowmenfashions/wwwroot/images/banners/` and Footer quality logos into `wowmenfashions/wwwroot/images/footer/`.

---

## Phase 2: Foundational (Blocking Prerequisites)

**Purpose**: Core infrastructure that MUST be complete before ANY user story can be implemented

**⚠️ CRITICAL**: No user story work can begin until this phase is complete

- [x] T002 Create migration script `wowmenfashions/Database/Tables/005_OrderWorkflow.sql` to add `CourierName`, `TrackingNumber`, and `TrackingUrl` to `Orders` table.
- [x] T003 Update stored procedure `wowmenfashions/Database/StoredProcedures/dbo.Order_GetByEmail.sql` to select the new courier tracking columns.
- [x] T004 Create stored procedure `wowmenfashions/Database/StoredProcedures/dbo.Order_UpdateStatus.sql` for status transitions.
- [x] T005 Create stored procedure `wowmenfashions/Database/StoredProcedures/dbo.Order_UpdateTracking.sql` to save courier details.
- [x] T006 Update `wowmenfashions/Models/OrderDto.cs` to include `CourierName`, `TrackingNumber`, and `TrackingUrl`.
- [x] T007 Update `wowmenfashions/Services/IOrderService.cs` with the new tracking and status methods.
- [x] T008 Update `wowmenfashions/Services/OrderService.cs` with the Dapper implementation of the tracking and status methods.

**Checkpoint**: Foundation ready - user story implementation can now begin in parallel

---

## Phase 3: User Story 1 - Track Order Status (Priority: P1)

**Goal**: Allow customers to track the status of their order and view courier tracking links.

**Independent Test**: Create an order, view order history, see status. Manually inject tracking URL in DB, view order details, see tracking link.

### Implementation for User Story 1

- [x] T009 [US1] Update `wowmenfashions/Components/Pages/Account/Orders.razor` to display a status badge (Placed, Processing, etc.) and Courier tracking link if `TrackingUrl` is not null.

**Checkpoint**: At this point, User Story 1 should be fully functional and testable independently

---

## Phase 4: User Story 2 - Homepage Navigation and Promotions (Priority: P1)

**Goal**: Redesign the homepage with engaging promotions and product grids.

**Independent Test**: Navigate to homepage, view RFID banner, zip around wallet banner, and product grids.

### Implementation for User Story 2

- [x] T010 [US2] Update `wowmenfashions/Components/Pages/Home.razor` to implement the new layout using `MudGrid`, rendering the RFID banner, categories, and zip wallet banner matching the screenshots.

**Checkpoint**: At this point, User Stories 1 AND 2 should both work independently

---

## Phase 5: User Story 3 - Enhanced Product Details (Priority: P2)

**Goal**: Zoom in on product images and select colors using square color boxes.

**Independent Test**: Visit a product details page, hover over the image to zoom, and click the square color boxes.

### Implementation for User Story 3

- [x] T011 [US3] Update `wowmenfashions/Components/Pages/ProductDetails.razor` to replace `MudChip` color selection with custom CSS-styled square boxes.
- [x] T012 [US3] Update `wowmenfashions/Components/Pages/ProductDetails.razor` (and related CSS if needed) to implement CSS-based hover zoom on the product image.

**Checkpoint**: All user stories should now be independently functional

---

## Phase 6: User Story 4 - Brand Trust Footer (Priority: P3)

**Goal**: Show brand quality indicators in the footer.

**Independent Test**: Scroll to the bottom of any page and view the quality banners and dark footer area.

### Implementation for User Story 4

- [x] T013 [US4] Update `wowmenfashions/Components/Layout/MainLayout.razor` to implement the new footer containing the quality certification logos and dark section with brand links.

**Checkpoint**: All user stories should now be independently functional

---

## Phase 7: Polish & Cross-Cutting Concerns

**Purpose**: Improvements that affect multiple user stories

- [x] T014 Run quickstart.md validation tests manually to ensure everything works end-to-end.

---

## Dependencies & Execution Order

### Phase Dependencies

- **Setup (Phase 1)**: No dependencies - can start immediately
- **Foundational (Phase 2)**: Depends on Setup completion - BLOCKS all user stories
- **User Stories (Phase 3+)**: All depend on Foundational phase completion
- **Polish (Final Phase)**: Depends on all desired user stories being complete
