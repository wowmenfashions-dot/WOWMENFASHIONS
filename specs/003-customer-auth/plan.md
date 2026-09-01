# Implementation Plan: Custom Authentication and Customer Account

**Branch**: `003-customer-auth` | **Date**: 2026-08-30 | **Spec**: [spec.md](file:///c:/MyDrive/ProjectDrive/WOWMENFASHIONS/specs/003-customer-auth/spec.md)

## Summary

Implement a custom authentication module using HttpOnly cookies and BCrypt/PBKDF2 for password hashing, bypassing ASP.NET Core Identity to adhere to the project's Modular Monolith and Dapper-only architecture. 

## Technical Context

**Language/Version**: C# 12, .NET 10

**Primary Dependencies**: Blazor Server, MudBlazor, Dapper, Microsoft.Data.SqlClient, Microsoft.AspNetCore.Authentication.Cookies, BCrypt.Net-Next (or equivalent for hashing)

**Storage**: Microsoft SQL Server (via Stored Procedures)

**Testing**: N/A for this phase, manual UI testing.

**Target Platform**: Web (ASP.NET Core)

**Project Type**: Web application (Modular Monolith)

**Constraints**:
- NO Entity Framework
- ALL database operations must use Dapper + Stored Procedures
- NO ASP.NET Core Identity
- MUST implement Custom Auth with secure cookies.

## Constitution Check

*GATE: Passed*
- Does not use EF Core.
- Does not use Identity.
- Relies purely on Dapper and Stored Procedures.
- Modular Monolith architecture maintained.

## Project Structure

### Documentation (this feature)

```text
specs/003-customer-auth/
├── plan.md              # This file
├── research.md          # Custom authentication strategy
├── data-model.md        # Customer entity definitions
├── quickstart.md        # How to test auth flow
└── tasks.md             # (To be generated later)
```

### Source Code (repository root)

```text
wowmenfashions/
├── Data/
│   ├── Entities/
│   │   └── Customer.cs
│   ├── ICustomerRepository.cs
│   └── CustomerRepository.cs
├── Services/
│   ├── ICustomerService.cs
│   ├── CustomerService.cs
│   ├── IPasswordHasher.cs
│   └── PasswordHasher.cs
├── Database/
│   ├── Tables/
│   │   └── 003_CustomerTables.sql
│   └── StoredProcedures/
│       ├── dbo.Customer_Create.sql
│       ├── dbo.Customer_GetByEmail.sql
│       ├── dbo.Customer_GetById.sql
│       └── dbo.Customer_UpdateLastLogin.sql
├── Components/
│   └── Pages/
│       ├── Account/
│       │   ├── Login.razor
│       │   ├── Register.razor
│       │   └── Profile.razor
```

**Structure Decision**: Integrated directly into the Blazor Server modular monolith structure, adding Data, Services, and UI pages for authentication.
