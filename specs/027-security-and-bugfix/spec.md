# Feature Specification: Security Hardening & Bug Fixes

**Feature Branch**: `[027-security-and-bugfix]`
**Created**: 2026-09-01
**Status**: Approved

## User Scenarios & Testing

### User Story 1 - Secure Admin Area (Priority: P1)
As an Admin, I want my admin dashboard to be highly secure, so that no unauthorized user can access it, and my session is protected from common web vulnerabilities.
**Acceptance Scenarios**:
1. **Given** a user is not an Admin, **When** they try to access any page under `/admin`, **Then** they are blocked.
2. **Given** a user is logging in, **When** they spam the login endpoint, **Then** they are rate-limited.

### User Story 2 - Base64 Image Uploads (Priority: P1)
As an Admin, I want to paste base64 images into the Image URL field without the database crashing due to string truncation.
**Acceptance Scenarios**:
1. **Given** I paste a large base64 string, **When** I save the product, **Then** the product saves successfully.

## Requirements

### Functional Requirements
- **FR-001**: System MUST enforce `[Authorize(Roles = "Admin")]` on all files in `Components/Pages/Admin` via `_Imports.razor`.
- **FR-002**: System MUST configure auth cookies with HttpOnly, Secure, SameSite=Strict, and sliding expiration.
- **FR-003**: System MUST implement ASP.NET Core Rate Limiting for the `/admin/login` route.
- **FR-004**: System MUST alter the `Products` table `ImageUrl` column to `NVARCHAR(MAX)`.
- **FR-005**: System MUST validate the "Admin" role inside critical backend services (e.g., ProductService) using `IHttpContextAccessor`.
- **FR-006**: System MUST create an `AdminAuditLogs` table and log actions.

## Success Criteria
- **SC-001**: Admin folder is protected globally.
- **SC-002**: The bug causing SQL truncation for `ImageUrl` is resolved.
