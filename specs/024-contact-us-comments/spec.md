# Feature Specification: Contact Us Comments & Admin Panel

**Feature Branch**: `[024-contact-us-comments]`
**Created**: 2026-09-01
**Status**: Approved

## User Scenarios & Testing

### User Story 1 - Submit Contact Message (Priority: P1)
As a visitor, I can leave a comment/message via the Contact Us page so that the site admins can read and respond to my inquiries.
**Acceptance Scenarios**:
1. **Given** I am on the Contact Us page, **When** I fill out Name, Email, Subject, and Message and click Submit, **Then** my message is saved to the database with 'Unread' status and I see a success notification.

### User Story 2 - Admin Views Messages (Priority: P1)
As an admin, I can view all user messages on a dedicated admin page so I can manage customer inquiries.
**Acceptance Scenarios**:
1. **Given** I am on the Admin Contact Messages page, **When** I view the page, **Then** I see two tabs: 'Unread' and 'Read'.
2. **Given** I am on the 'Unread' tab, **When** I click 'Mark as Read' on a message, **Then** the message status is updated in the database and it moves to the 'Read' tab.

### User Story 3 - Redesign About & Contact Pages (Priority: P2)
As a visitor, I see a redesigned About Us and Contact Us page that matches the provided screenshot structure.
**Acceptance Scenarios**:
1. **Given** I am on the About Us page, **When** I view it, **Then** I see a centered title, text content, and a 4-column feature highlights section at the bottom (Free Shipping, Mobile App, World Wide, Fashion Design).

## Requirements

### Functional Requirements
- **FR-001**: System MUST provide a form on the Contact Us page for users to submit messages (Name, Email, Subject, Message).
- **FR-002**: System MUST persist submitted messages to the database with a default status of Unread (IsRead = 0).
- **FR-003**: System MUST provide an Admin UI to view all messages, separated by Unread and Read statuses.
- **FR-004**: System MUST allow Admins to mark an Unread message as Read.
- **FR-005**: System MUST present a redesigned About Us page and Contact Us page matching the screenshot.

### Key Entities
- **ContactMessage**: Id, Name, Email, Subject, Message, IsRead, CreatedAt.

## Success Criteria
- **SC-001**: A user can successfully submit a contact message and an admin can view and mark it as read.
- **SC-002**: The UI matches the requested layout visually.
