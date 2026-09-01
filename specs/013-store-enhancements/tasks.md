# Tasks: 013-store-enhancements

## Phase 1: Database Setup
- [ ] T001 Create and execute `Database\05_AddCategories.sql` to insert `Kids` and `Toddler` categories.

## Phase 2: Feature Implementation

### Default Currency (US3)
- [ ] T002 Modify `wowmenfashions\Program.cs` to set the default culture to `en-IN` (INR).

### Auto-fill Shipping Details (US1)
- [ ] T003 Update `wowmenfashions\Components\Pages\Checkout.razor` to retrieve `AuthenticationStateProvider` and `ICustomerService`.
- [ ] T004 In `Checkout.razor`, check if the user is authenticated, and if so, map their profile data to `CheckoutModel`.

### Accurate Order Summary (US2)
- [ ] T005 Update `Checkout.razor` order summary calculations to include 10% tax.
- [ ] T006 Update `TotalAmount` calculation in `Checkout.razor` to correctly sum Subtotal, Tax, Shipping, minus Discount.

## Phase 3: UI Updates (US4)
- [ ] T007 Modify `wowmenfashions\Components\Layout\NavMenu.razor` to statically add `Kids` and `Toddler` to the navigation menu, OR ensure they are dynamically loaded if applicable (currently they are static in NavMenu).
