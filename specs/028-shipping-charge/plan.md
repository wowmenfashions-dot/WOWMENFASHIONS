# Implementation Plan: Shipping Charge Threshold

**Branch**: `[028-shipping-charge]` | **Date**: 2026-09-02 | **Spec**: [spec.md](file:///c:/MyDrive/ProjectDrive/WOWMENFASHIONS/specs/028-shipping-charge/spec.md)

**Input**: Feature specification from `/specs/028-shipping-charge/spec.md`

## Summary

Implement a configurable shipping charge where a fee is added to the cart if the subtotal is less than a minimum threshold. Admins will be able to configure both the threshold and the charge amount via the admin dashboard.

## Technical Context

**Language/Version**: C# / .NET 8 (Blazor Server)
**Primary Dependencies**: Dapper, MudBlazor
**Storage**: SQL Server
**Testing**: Manual / Integration tests
**Target Platform**: Web Server
**Project Type**: E-Commerce Web Application
**Constraints**: Follow constitution: SPs only, Dapper only, no EF.

## Constitution Check

*GATE: Must pass before Phase 0 research. Re-check after Phase 1 design.*

- Must use SQL Server as the only database (Passed)
- Must use Dapper as the only data-access library (Passed)
- Must use Stored Procedures for all database operations (Passed)
- Database access must be isolated (Passed)

## Project Structure

### Documentation (this feature)

```text
specs/028-shipping-charge/
├── plan.md              # This file (/speckit-plan command output)
├── research.md          # Phase 0 output (/speckit-plan command)
├── data-model.md        # Phase 1 output (/speckit-plan command)
├── quickstart.md        # Phase 1 output (/speckit-plan command)
├── contracts/           # Phase 1 output (/speckit-plan command)
└── tasks.md             # Phase 2 output (/speckit-tasks command - NOT created by /speckit-plan)
```

### Source Code (repository root)

```text
wowmenfashions/
├── Models/
│   └── StoreSettingsDto.cs
├── Services/
│   ├── IStoreSettingsService.cs
│   └── StoreSettingsService.cs
├── Repositories/
│   ├── IStoreSettingsRepository.cs
│   └── StoreSettingsRepository.cs
├── Components/
│   └── Pages/
│       ├── Admin/
│       │   └── StoreSettings.razor
│       ├── Checkout.razor (modify)
│       └── Cart.razor (modify)
└── Components/
    └── Layout/
        └── AdminNavMenu.razor (modify)
```

**Structure Decision**: Added new `StoreSettings` feature components adhering to the modular monolith pattern, introducing a repository, service, and DTO for settings, and a new Admin UI page.
