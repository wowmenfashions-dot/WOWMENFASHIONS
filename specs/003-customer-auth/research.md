# Research: Custom Authentication

## Objective
Determine the best approach to implement custom authentication in a Blazor Server application without using ASP.NET Core Identity.

## Decision: Cookie-Based Authentication
**Decision**: Use ASP.NET Core's built-in Cookie Authentication handler (`AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)`) combined with a custom user service and password hasher.

**Rationale**: 
- Blazor Server integrates natively with ASP.NET Core's `HttpContext` during the initial connection and supports `AuthenticationStateProvider`.
- It securely handles HttpOnly cookies and session lifetime.
- It fully satisfies the constitution requirement of avoiding ASP.NET Core Identity (which includes EF Core dependencies and overly complex user stores).

## Decision: Password Hashing
**Decision**: Use `BCrypt.Net-Next` or standard .NET `Rfc2898DeriveBytes` (PBKDF2) for password hashing.
**Rationale**: 
- Secure, industry standard.
- Avoids storing plaintext passwords as mandated by the requirements.

## Constitution Alignment
- **Dapper & SPs**: The `CustomerRepository` will execute `dbo.Customer_GetByEmail` and `dbo.Customer_Create` using Dapper.
- **Isolation**: The `AuthenticationStateProvider` will interact with `ICustomerService` to validate claims, keeping DB logic strictly out of the UI.
