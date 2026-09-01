# Feature Specification: Stage 2 - Shopping Cart & Guest Cart

**Feature Branch**: `[002-shopping-cart]`

**Created**: 2026-08-30

**Status**: Draft

**Input**: User description: "/speckit.specify --source @[c:\MyDrive\ProjectDrive\WOWMENFASHIONS\requirements\requirement.txt]"

## User Scenarios & Testing *(mandatory)*

### User Story 1 - Add Product to Cart (Priority: P1)

As a guest visitor, I want to add products to my shopping cart so that I can collect items I intend to purchase.

**Why this priority**: Core interaction for e-commerce.

**Independent Test**: Can be tested by clicking "Add to Cart" on a product and verifying the cart state updates correctly.

**Acceptance Scenarios**:

1. **Given** a product with available inventory, **When** I click "Add to Cart", **Then** the product is added to my guest cart.
2. **Given** I already have a product in my cart, **When** I add the same product again, **Then** the quantity of that item in the cart increases.
3. **Given** a product has insufficient inventory, **When** I try to add a quantity greater than available, **Then** I am prevented from doing so.

---

### User Story 2 - View and Manage Cart (Priority: P1)

As a guest visitor, I want to view my shopping cart and modify quantities or remove items so that I can control my pending order.

**Why this priority**: Essential for finalizing the purchase decision.

**Independent Test**: Can be tested by navigating to `/cart` and interacting with the cart items.

**Acceptance Scenarios**:

1. **Given** I have items in my cart, **When** I navigate to the cart page, **Then** I see all items, unit prices, item totals, and the overall cart subtotal.
2. **Given** I am on the cart page, **When** I change the quantity of an item, **Then** the server recalculates the totals and validates against available inventory.
3. **Given** I am on the cart page, **When** I click remove on an item, **Then** it is completely removed from the cart.

---

### User Story 3 - Cart Persistence across Sessions (Priority: P2)

As a guest visitor, I want my cart to persist across page reloads and browsing sessions so that I don't lose my selected items if I close the tab.

**Why this priority**: Greatly improves conversion rate and user experience.

**Independent Test**: Can be tested by adding items, refreshing the page or restarting the browser session, and verifying items remain.

**Acceptance Scenarios**:

1. **Given** I have added items to my guest cart, **When** I refresh the browser or navigate away, **Then** my cart contents remain intact upon return.

---

### User Story 4 - Checkout Authentication Enforcement (Priority: P1)

As a guest visitor, I want to be prompted to log in or register when I proceed to checkout so that my order can be processed securely.

**Why this priority**: Meets the business requirement that authentication is mandatory for purchasing.

**Independent Test**: Can be tested by clicking "Proceed to Checkout" from a guest cart and verifying redirection.

**Acceptance Scenarios**:

1. **Given** I have items in my guest cart, **When** I click "Proceed to Checkout", **Then** I am redirected to the authentication/login page.

### Edge Cases

- What happens if a product's price changes or it becomes inactive while sitting in a user's cart?
- How is the cart handled if the user clears their browser cookies?
- What happens if inventory is reduced by another user while an item is in the cart?

## Requirements *(mandatory)*

### Functional Requirements

- **FR-001**: System MUST allow users to add products to a cart without being authenticated.
- **FR-002**: System MUST identify guest carts using a secure browser-side identifier (e.g., cookie).
- **FR-003**: System MUST validate product existence, active status, and available inventory before adding to or updating the cart.
- **FR-004**: System MUST calculate all cart totals (item total, subtotal) server-side and never trust client-provided pricing.
- **FR-005**: System MUST verify current product pricing and availability when displaying or modifying the cart.
- **FR-006**: System MUST maintain a persistent guest cart with a configurable expiration period.
- **FR-007**: System MUST provide a main navigation indicator showing the current cart item count.
- **FR-008**: System MUST redirect unauthenticated users to the Login/Register flow when they attempt to proceed to checkout.

### Key Entities

- **Cart**: Represents a pending order state for either a guest or authenticated customer.
- **CartItem**: Represents a specific product and requested quantity within a cart.

## Success Criteria *(mandatory)*

### Measurable Outcomes

- **SC-001**: Users can add, modify, and remove cart items without requiring an account.
- **SC-002**: Guest cart state survives browser refreshes and page navigations.
- **SC-003**: 100% of price calculations and inventory validations occur server-side.
- **SC-004**: Users are reliably forced to authenticate before they can enter the checkout flow.

## Assumptions

- Cart items will not reserve inventory until the checkout/order creation phase.
- Pricing displayed in the cart will always reflect the current database price, meaning totals may change if a price update occurs while the item is in the cart.
