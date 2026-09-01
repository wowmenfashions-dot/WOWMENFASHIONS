# Feature Specification: Stage 3 - Custom Authentication and Customer Account

**Feature Branch**: `003-customer-auth`

**Created**: 2026-08-30

**Status**: Draft

**Input**: User description: "stage 3" (Sourced from requirements.txt)

## User Scenarios & Testing *(mandatory)*

### User Story 1 - Customer Registration (Priority: P1)

A guest user wants to register for a new account so they can proceed to checkout and manage their profile.

**Why this priority**: Without registration, no customer accounts can exist, blocking the rest of the application flow.

**Independent Test**: Can be fully tested by submitting the registration form with valid data and verifying a new customer record is created.

**Acceptance Scenarios**:

1. **Given** I am a guest user on the registration page, **When** I submit valid details, **Then** my account is created and my email is registered.
2. **Given** I submit an email that already exists, **When** I attempt to register, **Then** the system displays an error message preventing duplicate accounts.

---

### User Story 2 - Customer Login (Priority: P1)

A registered customer wants to log into their account using their email and password to access their profile and checkout.

**Why this priority**: Required for authenticated functionality.

**Independent Test**: Can be tested by providing correct credentials and verifying the server issues an authenticated session cookie.

**Acceptance Scenarios**:

1. **Given** I am on the login page, **When** I submit correct credentials, **Then** I am logged in and my session is persisted.
2. **Given** I submit an incorrect password, **When** I attempt to login, **Then** I see an error message and access is denied.

---

### User Story 3 - Customer Logout (Priority: P2)

An authenticated customer wants to log out to secure their account on a shared device.

**Why this priority**: Fundamental security requirement for session lifecycle.

**Independent Test**: Can be tested by clicking logout and verifying the session cookie is invalidated.

**Acceptance Scenarios**:

1. **Given** I am logged in, **When** I click "Logout", **Then** my session is terminated and I am redirected to the homepage as a guest.

---

### User Story 4 - Forgot Password (Priority: P3)

A customer who forgot their password needs to request a reset link to regain access.

**Why this priority**: Common usability requirement to prevent account lockouts.

**Independent Test**: Requesting a reset link generates a valid, expiring token.

**Acceptance Scenarios**:

1. **Given** I am on the forgot password page, **When** I submit my email, **Then** I receive a reset link if the email exists.
2. **Given** I use a valid reset link, **When** I submit a new password, **Then** my password is changed successfully.

---

### User Story 5 - Profile Management (Priority: P3)

An authenticated customer wants to update their personal details (name, phone) on their account page.

**Why this priority**: Allows users to keep their information up to date.

**Independent Test**: Updating profile fields reflects the new data on subsequent page loads.

**Acceptance Scenarios**:

1. **Given** I am logged in, **When** I update my phone number on the profile page, **Then** the new phone number is saved to my account.

---

## Requirements *(mandatory)*

### Functional Requirements

- **FR-001**: System MUST NOT use ASP.NET Core Identity; a custom authentication module is required.
- **FR-002**: System MUST hash passwords securely before storage. Plaintext passwords MUST NEVER be stored.
- **FR-003**: System MUST use secure server-side cookies (HttpOnly, Secure flag where applicable) for session management.
- **FR-004**: System MUST distinguish between Guest, Customer, and Administrator roles.
- **FR-005**: System MUST enforce unique email addresses for all customer accounts.
- **FR-006**: Password reset tokens MUST be single-use and have a configurable expiration time.
- **FR-007**: System MUST record the `LastLoginAt` timestamp upon successful authentication.
- **FR-008**: System MUST support email verification workflows.

### Key Entities

- **Customer**: Represents a registered user. Attributes: Id, FirstName, LastName, Email, Phone, PasswordHash, IsEmailVerified, IsActive, CreatedAt, UpdatedAt, LastLoginAt.

## Success Criteria *(mandatory)*

### Measurable Outcomes

- **SC-001**: 100% of new registrations are securely hashed in the database.
- **SC-002**: Users can complete registration and login flows in under 2 minutes.
- **SC-003**: Session cookies are successfully invalidated immediately upon logout.
- **SC-004**: Unauthorized access to customer-only pages (e.g., `/account/profile`) is prevented and redirects to `/login`.

## Assumptions

- Email delivery (for verification and password resets) will use a mock service or output to logs for now, until a formal SMTP provider is configured.
- Existing database architecture rules (Dapper + Stored Procedures) apply fully to the authentication module.
