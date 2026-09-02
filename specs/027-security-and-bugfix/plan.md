# Implementation Plan: Security Hardening & Bug Fixes

## 1. Database Fixes
- Create a SQL script `027_update_imageurl.sql` to `ALTER TABLE Products ALTER COLUMN ImageUrl NVARCHAR(MAX)`.
- Create `AdminAuditLogs` table (Id, Action, AdminEmail, Timestamp, EntityId) and a stored procedure `sp_LogAdminAction`.
- Execute the scripts.

## 2. Folder-Level Authorization
- Create `wowmenfashions/Components/Pages/Admin/_Imports.razor` containing:
  ```razor
  @using Microsoft.AspNetCore.Authorization
  @attribute [Authorize(Roles = "Admin")]
  ```
- Remove redundant `@attribute [Authorize(Roles = "Admin")]` from individual Admin pages.

## 3. Cookie Hardening & Rate Limiting
- In `Program.cs`:
  - Update cookie config: `HttpOnly = true`, `SecurePolicy = Always`, `SameSite = Strict`. Set expiration to 60 minutes with sliding expiration.
  - Add `builder.Services.AddRateLimiter(...)` with a fixed window limiter.
  - Add `app.UseRateLimiter()`.
  
## 4. Service-Level Security & Auditing
- Create `IAdminAuditService` / `AdminAuditService` to log actions.
- In `ProductService`, inject `IHttpContextAccessor` and `IAdminAuditService`. 
- In `CreateProductAsync`, check `_httpContextAccessor.HttpContext?.User.IsInRole("Admin")`. If false, throw `UnauthorizedAccessException`. After successful creation, log the action.
