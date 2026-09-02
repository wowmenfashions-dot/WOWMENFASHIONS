# Implementation Tasks: Checkout Address Format Update

## Phase 1: Setup (Shared Infrastructure)

**Purpose**: Project initialization and basic structure
(Project is already initialized, so no setup tasks are needed)

---

## Phase 2: Foundational (Blocking Prerequisites)

**Purpose**: Core infrastructure that MUST be complete before ANY user story can be implemented

**CRITICAL**: No user story work can begin until this phase is complete

- [x] T001 Create and run SQL migration to add granular fields to `Orders` and `CustomerAddresses` tables in `Database/Tables/007_AddressFields.sql`

**Checkpoint**: Foundation ready - user story implementation can now begin

---

## Phase 3: User Story 1 - Enter Shipping Address at Checkout (Priority: P1) (MVP)

**Goal**: Customers input detailed shipping details using distinct fields.

**Independent Test**: Can be tested by filling out the checkout form and observing validation triggers and successful order placement.

### Implementation for User Story 1

- [x] T002 [US1] Update `CheckoutModel` with new fields in `wowmenfashions/Models/CheckoutModel.cs`
- [x] T003 [P] [US1] Update `OrderDto` with new fields in `wowmenfashions/Models/OrderDto.cs`
- [x] T004 [US1] Update `dbo.Order_Create` stored procedure to handle new fields in `Database/04_CheckoutSystem.sql`
- [x] T005 [US1] Update `CheckoutService` to map the expanded address fields in `wowmenfashions/Services/CheckoutService.cs`
- [x] T006 [US1] Update `Checkout.razor` UI to display granular address fields in `wowmenfashions/Components/Pages/Checkout.razor`

**Checkpoint**: At this point, User Story 1 should be fully functional and testable independently

---

## Phase 4: User Story 2 - Address Storage and Display (Priority: P2)

**Goal**: Display and store the detailed addresses for registered customers and admin views.

**Independent Test**: Admins can view granular order details, and registered customers can save/view granular addresses.

### Implementation for User Story 2

- [x] T007 [P] [US2] Update `CustomerAddress` entity in `wowmenfashions/Data/Entities/CustomerAddress.cs`
- [x] T008 [P] [US2] Update `CustomerAddressDto` in `wowmenfashions/Models/CustomerAddressDto.cs`
- [x] T009 [US2] Update `dbo.Address_Create` stored procedure in `wowmenfashions/Database/StoredProcedures/dbo.Address_Create.sql`
- [x] T010 [US2] Update `dbo.Address_Update` stored procedure in `wowmenfashions/Database/StoredProcedures/dbo.Address_Update.sql`
- [x] T011 [US2] Update `Profile.razor` UI to show new fields in `wowmenfashions/Components/Pages/Account/Profile.razor`

**Checkpoint**: At this point, User Stories 1 AND 2 should both work independently

---

## Phase 5: Polish & Cross-Cutting Concerns

**Purpose**: Improvements that affect multiple user stories

- [ ] T012 Run quickstart validation from `quickstart.md`
- [ ] T013 Ensure all new forms support MudBlazor validation correctly

---

## Dependencies & Execution Order

### Phase Dependencies

- **Setup (Phase 1)**: N/A
- **Foundational (Phase 2)**: Starts immediately
- **User Stories (Phase 3+)**: All depend on Foundational phase completion
- **Polish (Final Phase)**: Depends on all user stories being complete

### User Story Dependencies

- **User Story 1 (P1)**: Can start after Foundational
- **User Story 2 (P2)**: Can start after Foundational, can be done parallel to US1 but UI depends on data models.

### Parallel Opportunities

- T002, T003, T007, T008 can be executed in parallel since they are distinct model files.
- Stored procedure updates (T004, T009, T010) can be executed in parallel.
