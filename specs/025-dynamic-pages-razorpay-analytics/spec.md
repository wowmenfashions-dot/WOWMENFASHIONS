# Feature Specification: Dynamic Pages, Razorpay, & Analytics

**Feature Branch**: `[025-dynamic-pages-razorpay]`
**Created**: 2026-09-01
**Status**: Approved

## User Scenarios & Testing

### User Story 1 - Dynamic Page Content (Priority: P1)
As an Admin, I can modify the content of the About Us and Contact Us pages from the Admin dashboard, so I don't need code deployments for text changes.
**Acceptance Scenarios**:
1. **Given** I am on the Admin Page Content editor, **When** I update the text/HTML for "AboutUs" and save, **Then** the public About Us page immediately reflects the new content.

### User Story 2 - Razorpay Checkout (Priority: P1)
As a Customer, I can pay for my cart items securely using Razorpay (in INR).
**Acceptance Scenarios**:
1. **Given** I am checking out, **When** I choose to pay, **Then** the Razorpay modal opens, processes the INR payment, and my order is placed upon success.

### User Story 3 - Google Analytics (Priority: P2)
As a Site Owner, I want Google Analytics tracking on the site to monitor traffic.
**Acceptance Scenarios**:
1. **Given** I view the site source, **Then** I see the Google Analytics gtag placeholder script in the head.

## Requirements

### Functional Requirements
- **FR-001**: System MUST store HTML content for dynamic pages in the database.
- **FR-002**: System MUST provide an Admin UI to edit the HTML content.
- **FR-003**: System MUST render the dynamic HTML on the public About Us and Contact Us pages.
- **FR-004**: System MUST integrate Razorpay JS checkout for order payments using INR.
- **FR-005**: System MUST include a Google Analytics placeholder snippet.

### Key Entities
- **PageContent**: Id, PageName (e.g., 'AboutUs', 'ContactUs'), ContentHtml, LastUpdated.

## Success Criteria
- **SC-001**: Admin can edit and save page content successfully.
- **SC-002**: Razorpay checkout flow can be initiated (with placeholder API keys).
- **SC-003**: GA snippet is present in the document head.
