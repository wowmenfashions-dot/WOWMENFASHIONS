# Tasks: Checkout System

## Phase 1: Setup (Shared Infrastructure)

**Purpose**: Project initialization and basic structure

- [x] T001 [P] Ensure dummy product image exists in `wowmenfashions/wwwroot/images/dummy-product.avif`

---

## Phase 2: Foundational (Blocking Prerequisites)

**Purpose**: Core infrastructure that MUST be complete before ANY user story can be implemented

**⚠️ CRITICAL**: No user story work can begin until this phase is complete

- [x] T002 Setup database schema and stored procedures in `Database/04_CheckoutSystem.sql`
- [x] T003 [P] Create `OrderDto` and `OrderItemDto` models in `wowmenfashions/Models/OrderDto.cs`
- [x] T004 [P] Create `CheckoutModel` in `wowmenfashions/Models/CheckoutModel.cs` for UI binding
- [x] T005 [P] Implement `ICheckoutService` and `CheckoutService` in `wowmenfashions/Services/CheckoutService.cs` and register in `Program.cs`

**Checkpoint**: Foundation ready - user story implementation can now begin in parallel

---

## Phase 3: User Story 1 - Checkout Calculation and Cost Breakdown (Priority: P1) 🏆 MVP

**Goal**: As a customer proceeding to checkout, I want to see a clear breakdown of my order costs including shipping, GST, and potential coupon discounts.

**Independent Test**: Can be fully tested by adding items to the cart and proceeding to checkout to verify the subtotal, shipping cost, GST addition, and final total are mathematically correct.

### Implementation for User Story 1

- [x] T006 [P] [US1] Create `Checkout.razor` UI structure in `wowmenfashions/Components/Pages/Checkout.razor`
- [x] T007 [US1] Implement cost calculation logic (Shipping, GST, Subtotal, Final Total) in `Checkout.razor` and `CheckoutService.cs`
- [x] T008 [US1] Add coupon input and logic to simulate 10% discount in `Checkout.razor`

**Checkpoint**: At this point, User Story 1 should be fully functional and testable independently

---

## Phase 4: User Story 2 - Dummy Payment Gateway Simulation (Priority: P1)

**Goal**: As a customer placing an order, I want to securely complete my purchase using a simulated payment gateway.

**Independent Test**: Can be fully tested by submitting the checkout form with valid details and observing a successful payment simulation and order confirmation.

### Implementation for User Story 2

- [x] T009 [P] [US2] Implement dummy payment processing (`Task.Delay`) and loading state UI in `Checkout.razor`
- [x] T010 [US2] Implement SQL insertion via Dapper in `CheckoutService.PlaceOrderAsync` (Calls Stored Procedures to save Order and OrderItems)
- [x] T011 [US2] Wire up the "Place Order" button to process payment, clear the cart, and show a success message

**Checkpoint**: At this point, User Stories 1 AND 2 should both work independently

---

## Phase 5: User Story 3 - Visual Fallbacks for Cart Items (Priority: P2)

**Goal**: As a customer reviewing my order, I want to see a dummy placeholder image for any product that lacks an image.

**Independent Test**: Can be fully tested by adding a product without an image to the cart and verifying the placeholder image is displayed.

### Implementation for User Story 3

- [x] T012 [P] [US3] Update `Checkout.razor` and cart views to fallback to `dummy-product.avif` when `ImageUrl` is null or empty

**Checkpoint**: All user stories should now be independently functional

---

## Phase 6: Polish & Cross-Cutting Concerns

**Purpose**: Improvements that affect multiple user stories

- [x] T013 Run `quickstart.md` validation scenarios

---

## Dependencies & Execution Order

### Phase Dependencies

- **Setup (Phase 1)**: No dependencies - can start immediately
- **Foundational (Phase 2)**: Depends on Setup completion - BLOCKS all user stories
- **User Stories (Phase 3+)**: All depend on Foundational phase completion
  - User stories can then proceed sequentially in priority order (P1 -> P2 -> P3)
- **Polish (Final Phase)**: Depends on all desired user stories being complete

### User Story Dependencies

- **User Story 1 (P1)**: Can start after Foundational (Phase 2)
- **User Story 2 (P1)**: Depends on User Story 1 checkout calculations and form
- **User Story 3 (P2)**: UI enhancement, can happen anytime after UI is built

## Implementation Strategy

### MVP First (User Story 1 & 2)

1. Complete Phase 1: Setup
2. Complete Phase 2: Foundational
3. Complete Phase 3: User Story 1
4. Complete Phase 4: User Story 2
5. **STOP and VALIDATE**: Test checkout flow fully.

### Incremental Delivery

1. Complete Setup + Foundational
2. Add User Story 1 (Calculations & UI) -> Test independently
3. Add User Story 2 (Payment & Order Saving) -> Test independently
4. Add User Story 3 (Fallback Image) -> Test independently
