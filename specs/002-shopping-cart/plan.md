# Implementation Plan: Stage 2 - Shopping Cart & Guest Cart

**Branch**: `[002-shopping-cart]` | **Date**: 2026-08-30 | **Spec**: [spec.md](./spec.md)

**Input**: Feature specification from `specs/002-shopping-cart/spec.md`

## Summary

Implement shopping cart functionality for guest users allowing them to browse, add to cart, and manage quantities without authenticating.

## Technical Context

**Language/Version**: C# / .NET

**Primary Dependencies**: Dapper (for data access), MudBlazor (UI framework)

**Storage**: SQL Server 
*(Connection String: `Data Source=(localdb)\MSSqlLocalDb;Initial Catalog=wowmenfashions;User ID=appuser;Password=Sandhya@143;TrustServerCertificate=True;Encrypt=False;Connect Timeout=30;`)*

**Testing**: xUnit, bUnit, database integration tests

**Target Platform**: Blazor Server Application

**Project Type**: Modular Monolith

**Constraints**: 
- **NO Entity Framework allowed**
- Dapper ONLY for data access
- Stored Procedures ONLY for database operations
- SQL Server is the sole authoritative data source

## Constitution Check

*GATE: Must pass before Phase 0 research. Re-check after Phase 1 design.*

- [x] Uses SQL Server + Dapper (Principle 1 & 2)
- [x] Database operations via Stored Procedures ONLY (Principle 3)
- [x] No direct SQL from application code (Principle 4)
- [x] Database access isolated via Repository Layer (Principle 5 & 6)
- [x] No Entity Framework (Principle 9)
- [x] Pagination, filtering, sorting handled in SQL Server (Principles 16 & 17)
- [x] Modular Monolith architecture maintained (Principle 26)

## Project Structure

### Documentation (this feature)

```text
specs/002-shopping-cart/
├── plan.md              # This file
├── research.md          
├── data-model.md        
├── quickstart.md        
└── tasks.md             # To be created by /speckit-tasks
```

### Source Code (repository root)

```text
wowmenfashions/
├── Components/
│   ├── Pages/
│   │   └── Cart.razor          # UI for shopping cart
│   └── Shared/
│       └── CartWidget.razor    # Navigation cart indicator
├── Services/
│   ├── ICartService.cs
│   └── CartService.cs          # Business logic
├── Data/
│   ├── ICartRepository.cs
│   └── CartRepository.cs       # Dapper data access
└── Database/
    └── StoredProcedures/
        ├── dbo.Cart_Get.sql
        ├── dbo.Cart_AddItem.sql
        ├── dbo.Cart_UpdateItem.sql
        └── dbo.Cart_RemoveItem.sql
```

**Structure Decision**: A standard Blazor Server layered structure. Components call Services, Services call Repositories, Repositories call Stored Procedures via Dapper.

## Complexity Tracking

*No constitution violations present. Standard layered architecture implemented.*
