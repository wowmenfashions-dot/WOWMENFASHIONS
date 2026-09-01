# Feature Specification: store-enhancements

**Feature Branch**: `[013-store-enhancements]`

**Created**: 2026-08-31

**Status**: Draft

**Input**: User description: "1] default currency is INR. 2] when i click on proceed to checkout. shipping details should be autofilled if user is logged in. if not they should enter the details. 3] order summary calculation is not proper. 4] why other category for kids , toddler is not gettiing displayed"

## User Scenarios & Testing *(mandatory)*

### User Story 1 - Checkout with Auto-filled Shipping (Priority: P1)

As a logged-in user, I want my shipping details to be pre-filled when I proceed to checkout, so I don't have to enter them manually every time.

**Why this priority**: Reduces friction in the checkout process, increasing conversion rates.

**Independent Test**: Can be fully tested by logging in, adding an item to the cart, navigating to checkout, and observing the pre-filled form fields.

**Acceptance Scenarios**:

1. **Given** I am logged into my account, **When** I navigate to the checkout page, **Then** my saved shipping address and contact details should be pre-populated in the checkout form.
2. **Given** I am a guest (not logged in), **When** I navigate to the checkout page, **Then** the shipping details form should be empty and ready for manual input.

---

### User Story 2 - Accurate Order Summary (Priority: P1)

As a customer, I want to see a mathematically correct order summary including subtotal, tax/GST, shipping, and any applied discounts.

**Why this priority**: Essential for transparency and billing accuracy.

**Independent Test**: Can be tested by verifying the calculations on the checkout page against a manual calculation of the items in the cart.

**Acceptance Scenarios**:

1. **Given** I have items in my cart, **When** I view the checkout summary, **Then** the Subtotal, Tax, Shipping Fee, and Total Amount must be calculated and displayed accurately.
2. **Given** I apply a discount coupon, **When** the cart re-calculates, **Then** the total amount should accurately reflect the reduced price.

---

### User Story 3 - Default Currency Display (Priority: P2)

As a user browsing the store, I want to see all product prices and cart totals displayed in INR (₹) by default.

**Why this priority**: Localizes the store for the primary customer base.

**Independent Test**: Can be tested by browsing product listings, details, and cart pages to ensure the INR symbol is used for formatting.

**Acceptance Scenarios**:

1. **Given** I view a product card, **When** the price is rendered, **Then** it is displayed in INR currency format.
2. **Given** I view my cart or checkout summary, **When** totals are shown, **Then** they are displayed in INR currency format.

---

### User Story 4 - Kids & Toddler Categories Visibility (Priority: P2)

As a shopper, I want to be able to browse products in the Kids and Toddler categories.

**Why this priority**: Expands the catalog visibility and product reach.

**Independent Test**: Can be tested by ensuring these categories appear in navigation or filtering and successfully list products.

**Acceptance Scenarios**:

1. **Given** the categories exist in the database, **When** I browse the store, **Then** I can see and select "Kids" and "Toddler" categories.
2. **Given** I select the Kids category, **When** the page loads, **Then** I see products belonging to that category.

### Edge Cases

- What happens if a logged-in user does not have a saved address yet? (The form remains blank for them to fill).
- What happens if no products exist for a category? (The category should still appear but show an "empty" message, or be hidden based on business rules).

## Requirements *(mandatory)*

### Functional Requirements

- **FR-001**: The application MUST set its default culture/currency formatting to INR (₹) globally for all price displays.
- **FR-002**: The checkout page MUST fetch the logged-in user's profile data (e.g., Address, Phone, Name) from the `ICustomerService` and populate the `CheckoutModel` automatically.
- **FR-003**: The checkout page MUST allow non-logged-in (guest) users to manually input their shipping details.
- **FR-004**: The order summary MUST correctly aggregate the Subtotal, Shipping Fee, Tax, and Discount Amount to form the final Total.
- **FR-005**: The system MUST query the database to ensure the 'Kids' and 'Toddler' categories exist and are exposed to the UI layer (e.g., NavMenu, Category filters).

### Key Entities

- **Customer / User Profile**: Contains saved shipping address and contact details.
- **CheckoutModel**: The form model that binds to the UI and holds shipping/billing data.
- **Category**: Represents a product grouping (e.g., Mens, Womens, Kids, Toddler).

## Success Criteria *(mandatory)*

### Measurable Outcomes

- **SC-001**: 100% of price displays across the app render using the INR symbol.
- **SC-002**: Logged-in users experience a 50% reduction in time spent on the checkout form due to auto-filling.
- **SC-003**: Mathematical validation passes for all cart combinations in the order summary without discrepancies.
- **SC-004**: Kids and Toddler categories are accessible from the main navigation or category lists.

## Assumptions

- Users who log in have their profile information saved in the database under `Customers`.
- "Order summary calculation is not proper" refers to the current UI logic in `Checkout.razor` which may have hardcoded values or missing tax logic.
- "Kids" and "Toddler" categories may be missing from the database seeding script or the UI navigation menu.
