# Feature Specification: Shipping Charge Threshold

**Feature Branch**: `[028-shipping-charge]`

**Created**: 2026-09-02

**Status**: Draft

**Input**: User description: "New feature: I wan to add shipping charge if the amount is less than 1000 INR. Admin has a capability to change 1000 INR in the admin dashboard."

## User Scenarios & Testing *(mandatory)*

### User Story 1 - Apply Shipping Charge to Low Value Carts (Priority: P1)

As a customer, when my cart total is below the minimum threshold, I am charged a shipping fee so that small orders cover their delivery costs.

**Why this priority**: Core functional requirement for revenue and cost recovery.

**Independent Test**: Can be tested by adding items below the threshold and observing the shipping charge in the cart/checkout, then adding more items to exceed the threshold and observing the charge disappear.

**Acceptance Scenarios**:

1. **Given** a cart total is strictly less than the configured free shipping threshold, **When** I view my cart or checkout, **Then** a shipping charge is added to my total.
2. **Given** a cart total is equal to or greater than the configured free shipping threshold, **When** I view my cart or checkout, **Then** no shipping charge is added (shipping is free).

---

### User Story 2 - Configure Shipping Threshold and Charge (Priority: P1)

As an admin, I want to configure the minimum threshold for free shipping and the shipping charge amount from the admin dashboard, so that I can adjust policies without code changes.

**Why this priority**: The user specifically requested admin capability to change the 1000 INR threshold. Admin must also be able to change the actual shipping cost itself (assumed default, maybe 50 or 100 INR).

**Independent Test**: Can be tested by logging into the admin dashboard, updating the settings, and saving.

**Acceptance Scenarios**:

1. **Given** I am an authenticated admin, **When** I navigate to the settings/configuration page, **Then** I see fields to update the "Free Shipping Threshold" and "Shipping Charge Amount".
2. **Given** I have changed the shipping settings, **When** I save the changes, **Then** the new values are immediately applied to all new customer carts.

### Edge Cases

- What happens if the threshold is set to 0? (Should mean free shipping for all orders).
- What happens to existing carts if the admin changes the threshold while a customer is shopping? (Typically, cart totals recalculate on checkout or cart page refresh).

## Requirements *(mandatory)*

### Functional Requirements

- **FR-001**: System MUST apply a shipping charge to a cart if the subtotal is less than the configured free shipping threshold.
- **FR-002**: System MUST provide an admin interface to configure the "Free Shipping Threshold" (default 1000 INR).
- **FR-003**: System MUST provide an admin interface to configure the "Shipping Charge" amount (default 100 INR).
- **FR-004**: System MUST store these configurations persistently (e.g., database or config file) so they survive application restarts.

### Key Entities *(include if feature involves data)*

- **StoreSettings/Configuration**: Needs to include `FreeShippingThreshold` and `ShippingChargeAmount`.

## Success Criteria *(mandatory)*

### Measurable Outcomes

- **SC-001**: Customers with carts under the threshold see the shipping charge correctly calculated in 100% of checkouts.
- **SC-002**: Admins can update the threshold and shipping charge in the admin dashboard in under 1 minute.
- **SC-003**: Threshold updates are reflected in customer carts immediately without requiring an application restart.

## Assumptions

- We assume there is an existing mechanism to store site-wide settings in the database, which we can extend.
- We assume the currency is INR as requested, but the system might just store numeric values.
- Default shipping charge is assumed to be a flat rate, not dynamic based on weight/distance.
