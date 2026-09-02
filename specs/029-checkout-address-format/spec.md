# Feature Specification: Checkout Address Format Update

**Feature Branch**: `029-checkout-address-format`

**Created**: 2026-09-02

**Status**: Draft

**Input**: User description: "I want the address to be like this
Address 1 
Address 2 (optional)
city state , county  defaulut to india)
postal code
contact person number
Near by landmark."

## User Scenarios & Testing *(mandatory)*

### User Story 1 - Enter Shipping Address at Checkout (Priority: P1)

Customers must be able to input their shipping details accurately at checkout using the new standardized format so their orders can be shipped correctly.

**Why this priority**: Accurate addresses and contact information are critical for order fulfillment and delivery. Without this, no order can be successfully shipped.

**Independent Test**: Can be fully tested by simulating a checkout flow and ensuring all the new fields accept data correctly and form a comprehensive address in the database.

**Acceptance Scenarios**:

1. **Given** a user is on the checkout page, **When** they view the address form, **Then** they see fields for Address 1 (required), Address 2 (optional), City, State, Country (defaulting to India), Postal Code, Contact Number, and Nearby Landmark.
2. **Given** a user leaves "Address 2" or "Nearby Landmark" empty, **When** they submit the form, **Then** the checkout proceeds successfully without validation errors for those fields.
3. **Given** a user leaves a mandatory field like "Contact Number" empty, **When** they submit the form, **Then** the form prevents submission and displays an appropriate validation error.

### User Story 2 - Address Storage and Display (Priority: P2)

Saved addresses for registered customers should conform to this new model and display correctly across the application (Order Summary, Admin Dashboard, Customer Profile).

**Why this priority**: Enhances the registered user experience so they don't have to re-enter details, and ensures Admins see the new format.

**Independent Test**: Can be tested by viewing an existing customer profile and an admin order detail page to confirm the new fields (State, Landmark, Contact Number) are rendered.

**Acceptance Scenarios**:

1. **Given** a customer has placed an order with the new format, **When** they view their order history, **Then** the address is formatted to include the landmark and state.
2. **Given** an admin views an order in the dashboard, **When** they look at the shipping details, **Then** they can see the contact person number and nearby landmark clearly.

## Requirements *(mandatory)*

### Functional Requirements

- **FR-001**: The checkout address form MUST include the following specific fields:
  - Address 1 (Required)
  - Address 2 (Optional)
  - City (Required)
  - State (Required)
  - Country (Required, default value "India")
  - Postal Code (Required)
  - Contact Person Number (Required)
  - Nearby Landmark (Optional)
- **FR-002**: The system MUST validate required fields and display clear error messages if omitted.
- **FR-003**: The Contact Person Number MUST accept valid phone number formats.
- **FR-004**: The CustomerAddress and Order schemas MUST store all these distinct fields to prevent data loss.
- **FR-005**: All UI surfaces displaying addresses (Admin dashboard, user profile, checkout summary) MUST be updated to accommodate the new fields.

### Key Entities 

- **CustomerAddress**: Represents a saved address for a user. Needs new fields (State, Landmark, ContactNumber).
- **Order**: Represents the placed order. Its ShippingAddress snapshot or related fields need to accommodate the detailed format.

## Success Criteria *(mandatory)*

### Measurable Outcomes

- **SC-001**: Users can successfully place orders with the new address format.
- **SC-002**: 100% of newly placed orders store the complete set of address fields without truncation.
- **SC-003**: Form validation correctly blocks 100% of submissions missing mandatory fields like the contact number.

## Assumptions

- Users currently have an address model that will need database schema updates (migrations/alter tables) to add the missing fields (State, Landmark, Contact Number, etc.).
- The default country "India" will be pre-selected in a text input or dropdown, but users can still change it if they need international shipping (unless restricted).
- Existing addresses in the database without these new fields will be treated gracefully (e.g., leaving the new fields null or prompting the user to update).
