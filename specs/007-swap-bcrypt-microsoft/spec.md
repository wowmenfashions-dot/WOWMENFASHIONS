# Feature Specification: Swap BCrypt for Microsoft PasswordHasher

**Feature Branch**: `007-swap-bcrypt-microsoft`

**Created**: 2026-08-30

**Status**: Draft

**Input**: User description: "Swap BCrypt (Optional): If you'd like to get slightly closer to a pure Microsoft stack, we can remove the BCrypt.Net-Next package and replace it with Microsoft's built-in PasswordHasher."

## User Scenarios & Testing *(mandatory)*

### User Story 1 - Use Microsoft Password Hashing (Priority: P1)

As a system, I want to use Microsoft's built-in `PasswordHasher` instead of the 3rd-party `BCrypt.Net-Next` package to reduce reliance on non-Microsoft 3rd-party libraries.

**Why this priority**: Directly requested by the user to align with the "Microsoft first" preference.

**Independent Test**: Register a new user, then log in with that user. The authentication flow should succeed seamlessly using the new hashing mechanism.

**Acceptance Scenarios**:

1. **Given** a user is registering, **When** their password is saved, **Then** it is hashed using `Microsoft.AspNetCore.Identity.PasswordHasher`.
2. **Given** a user is logging in, **When** they provide their password, **Then** it is verified successfully against the Microsoft hash format.

---

### Edge Cases
- Existing users with BCrypt hashes (if any) will not be able to log in unless backwards compatibility is maintained or the database is cleared. Since this is an early-stage project with seed data/development testing, we can assume existing user passwords can be reset or the database cleared. 

## Requirements *(mandatory)*

### Functional Requirements

- **FR-001**: Remove the `BCrypt.Net-Next` NuGet package from the `wowmenfashions` project.
- **FR-002**: Update the `IPasswordHasher` implementation (or equivalent service) to use `Microsoft.AspNetCore.Identity.PasswordHasher<TUser>`.
- **FR-003**: Ensure the registration and login flows continue to function correctly.

## Success Criteria *(mandatory)*

### Measurable Outcomes

- **SC-001**: The `BCrypt.Net-Next` package is completely absent from the `.csproj` file.
- **SC-002**: Registration and Login flows work without throwing cryptographic exceptions.
