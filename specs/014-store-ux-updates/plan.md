# Implementation Plan: Store UX Updates

**Branch**: `014-store-ux-updates` | **Date**: 2026-08-31 | **Spec**: N/A (Using tasks.md)

**Input**: Derived from tasks.md

## Summary

Update the store UX including:
- Customer addresses management UI (Multiple Addresses)
- Order history UI
- Product color selection via color swatches
- Category UI updates (Removing color dropdown, moving sort dropdown)
- Required database schema, model, and service updates to support the above features.

## Technical Context

**Language/Version**: C#, .NET 8, Blazor Server
**Primary Dependencies**: MudBlazor, Dapper
**Storage**: SQL Server
**Testing**: Unit Tests (if applicable)
**Target Platform**: Web (Blazor Server)
**Project Type**: Web Application
**Performance Goals**: Fast UI rendering, efficient database queries via Stored Procedures.
**Constraints**: Must strictly follow Constitution rules: Dapper + Stored Procedures ONLY. No EF Core. No raw SQL in C#.

## Constitution Check

*GATE: Must pass before Phase 0 research. Re-check after Phase 1 design.*

- **Principle 1**: SQL Server only. (PASS)
- **Principle 2 & 9 & 24**: Dapper only, NO EF Core. (PASS)
- **Principle 3 & 4 & 8**: Stored Procedures ONLY. (PASS - All DB changes must be via `.sql` migration scripts defining Stored Procedures).
- **Principle 5 & 6 & 7**: Blazor -> Service -> Repository -> Stored Procedure flow. (PASS)
- **Principle 29**: AVIF Images only. (PASS - UI changes won't introduce non-AVIF images).

## Project Structure

### Documentation (this feature)

```text
specs/014-store-ux-updates/
├── plan.md              # This file
├── data-model.md        # Data model changes
└── tasks.md             # Existing Tasks
```

### Source Code

```text
wowmenfashions/
├── Database/
│   ├── Tables/
│   └── StoredProcedures/
├── Data/
│   ├── Entities/
│   └── Repositories (ICartRepository, ICustomerRepository)
├── Models/
│   └── Dtos
├── Services/
│   └── ICustomerService, ICartService
└── Components/
    ├── Pages/
    │   ├── Profile.razor
    │   ├── Orders.razor
    │   ├── Checkout.razor
    │   ├── ProductDetails.razor
    │   └── Category.razor
```

**Structure Decision**: The project is a standard Blazor Server application using the Modular Monolith architecture, adhering to the structure outlined in the constitution.
