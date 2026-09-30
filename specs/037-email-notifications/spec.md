# Feature Specification: Order Email Notifications

**Feature Branch**: `037-email-notifications`

**Created**: 2026-09-27

**Status**: Draft

**Input**: User description: "when the user places order it should send email. and when update the status it should send an email on the status. My application is deployed in the azure. i want to use azure email components"

## User Scenarios & Testing *(mandatory)*

### User Story 1 - Order Confirmation Email (Priority: P1)

When a customer successfully completes checkout and payment is confirmed, they should automatically receive an order confirmation email at their registered email address. The email should summarise what they ordered, the total amount paid, and their delivery address so they have a clear record of the purchase.

**Why this priority**: This is the most critical communication touchpoint. Customers expect to receive order confirmation immediately after checkout. Without this, customers lack confidence the order was placed and may contact support unnecessarily.

**Independent Test**: Can be fully tested by placing a test order and verifying the confirmation email is received with accurate order details.

**Acceptance Scenarios**:

1. **Given** a customer successfully completes payment on the checkout page, **When** the payment is confirmed by Razorpay, **Then** an order confirmation email is sent to the customer's email address within 60 seconds.
2. **Given** an order confirmation email is sent, **When** the customer opens it, **Then** it contains: order ID, list of items with quantities and prices, total amount, shipping address, and a note that they will be notified when the order ships.
3. **Given** the email sending service is temporarily unavailable, **When** an order is placed, **Then** the order is still saved successfully and the email failure is logged without interrupting the checkout flow.

---

### User Story 2 - Order Status Update Email (Priority: P2)

When an admin updates the status of an order (e.g., from "Pending" to "Shipped" or "Delivered"), the customer should automatically receive an email notifying them of the new status. If the status is "Shipped", the email should also include the courier name, tracking number, and tracking URL (if available).

**Why this priority**: Keeping customers informed of their order progress reduces support enquiries and builds trust. Shipping notifications in particular are highly valued by online shoppers.

**Independent Test**: Can be fully tested by updating an order's status from the admin dashboard and verifying the customer receives an appropriate status update email.

**Acceptance Scenarios**:

1. **Given** an admin updates an order's status in the admin dashboard, **When** the status is saved, **Then** an email is sent to the customer notifying them of the new status.
2. **Given** an order status is updated to "Shipped", **When** the status update email is sent, **Then** it includes the courier name, tracking number, and a clickable tracking URL (if tracking details were entered).
3. **Given** an order status is updated to "Delivered", **When** the status update email is sent, **Then** it thanks the customer and invites them to leave a review.
4. **Given** an order status is updated to "Cancelled", **When** the status update email is sent, **Then** it informs the customer the order was cancelled and provides support contact information.

---

### User Story 3 - Admin Visibility of Email Status (Priority: P3)

An admin should be able to see whether notification emails were successfully sent for a given order, so they can manually follow up if an email failed to deliver.

**Why this priority**: Operational visibility reduces the risk of customers missing critical communications without anyone knowing.

**Independent Test**: Can be tested by triggering an email and checking the admin order detail view for a sent/failed indicator.

**Acceptance Scenarios**:

1. **Given** an email notification was successfully sent for an order, **When** an admin views that order, **Then** they see a "Confirmation email sent" indicator with a timestamp.
2. **Given** an email notification failed to send, **When** an admin views that order, **Then** they see a "Email failed to send" indicator so they can follow up manually.

---

### Edge Cases

- What happens when the customer's email address is invalid or the email bounces?
- How does the system handle a status being updated multiple times rapidly (e.g., "Pending" → "Processing" → "Shipped" in quick succession)?
- What if the Razorpay payment is confirmed but the database order save fails — should the confirmation email still send?
- What if an admin updates the order status but there is no customer email on record?

## Requirements *(mandatory)*

### Functional Requirements

- **FR-001**: System MUST send an order confirmation email to the customer automatically when payment is verified and the order is saved to the database.
- **FR-002**: System MUST send an order status update email to the customer automatically when an admin changes the order status in the admin dashboard.
- **FR-003**: The order confirmation email MUST contain: order ID, list of ordered items (name, quantity, unit price), total amount, and shipping address.
- **FR-004**: The order status update email MUST contain: order ID, the new status, and (for "Shipped" status) tracking details if available.
- **FR-005**: Email sending MUST be non-blocking — a failure to send an email MUST NOT prevent order creation or status updates from succeeding.
- **FR-006**: All email sending failures MUST be logged so that administrators can investigate and manually follow up.
- **FR-007**: The system MUST use Azure Communication Services Email as the email delivery mechanism, consistent with the application's Azure-hosted infrastructure.
- **FR-008**: Email templates MUST be branded with the WOWMENFASHIONS logo and styling, consistent with the website design.
- **FR-009**: System MUST record whether an email was sent successfully or failed for each order notification.

### Key Entities *(include if feature involves data)*

- **EmailNotification**: Represents a log record of an email sent for an order — includes order ID, notification type (confirmation/status-update), recipient email, sent timestamp, and success/failure status.
- **Order**: Extended to track whether a confirmation email has been sent.

## Success Criteria *(mandatory)*

### Measurable Outcomes

- **SC-001**: Order confirmation emails are received by customers within 60 seconds of a successful payment being confirmed.
- **SC-002**: Order status update emails are received by customers within 60 seconds of an admin updating the order status.
- **SC-003**: A failure to send an email does not cause any disruption to the order placement or admin order management workflow — 0 order failures attributable to email sending errors.
- **SC-004**: 100% of email send attempts (successful or failed) are logged and visible to the system administrator.
- **SC-005**: Email templates are visually consistent with the WOWMENFASHIONS brand on all major email clients (Gmail, Outlook, mobile).

## Assumptions

- Azure Communication Services Email will be provisioned in the same Azure subscription as the existing App Service and SQL Database.
- A verified sender domain or sender email address (e.g., `noreply@wowmenfashions.com` or an Azure-provided domain) will be configured in Azure Communication Services before implementation.
- Email notifications are sent in English only (no multi-language support required for this feature).
- The existing `Order_UpdateStatus` flow in the admin dashboard is the only trigger for status update emails — no automated status changes will trigger emails.
- Email sending will be handled asynchronously (via Hangfire background jobs already used in the application) to avoid blocking the UI.
- No unsubscribe/opt-out mechanism is required for this initial version, as emails are transactional (order-related) rather than marketing.
