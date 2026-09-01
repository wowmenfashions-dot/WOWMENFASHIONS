# Implementation Plan: State, Caching, and Cart

**Branch**: `006-state-caching-cart` | **Date**: 2026-08-30 | **Spec**: [spec.md](file:///c:/MyDrive/ProjectDrive/WOWMENFASHIONS/specs/006-state-caching-cart/spec.md)

**Input**: Feature specification from `/specs/006-state-caching-cart/spec.md`

## Summary

This feature resolves state leakage across category navigation, introduces `IMemoryCache` to optimize database calls for products and categories, and implements a functional Cart page (`/cart`) that was previously missing.

## Technical Context

**Language/Version**: C#, .NET 10.0

**Primary Dependencies**: Blazor Server, MudBlazor, Dapper (SQL), Microsoft.Extensions.Caching.Memory

**Storage**: SQL Server Database (via Dapper Stored Procedures)

**Testing**: Manual UI verification via quickstart.

**Target Platform**: Responsive Web (Mobile-First)

**Project Type**: Blazor Server Web Application

**Performance Goals**: Instantaneous category loads from cache.

**Constraints**: Data must ultimately originate from SQL; must adhere to Principle 5 (Stored Procedure isolated DB access).

**Scale/Scope**: Impacts `ProductService`, `Category.razor`, and introduces `Cart.razor`.

## Constitution Check

*GATE: Must pass before Phase 0 research. Re-check after Phase 1 design.*

- **Principle 5 (Isolated DB Access)**: PASS - All data fetches will continue to use existing Stored Procedures via Dapper.
- **Principle 23 (Technology Stack)**: PASS - Using standard .NET `IMemoryCache`.
- **Principle 28 (Error-Free Builds)**: PASS - Will ensure no analyzer warnings on new markup or C# code.
- **Principle 29 (Mobile-First Page Design)**: PASS - New cart page will be built mobile-first.

## Project Structure

### Documentation (this feature)

```text
specs/006-state-caching-cart/
├── plan.md              # This file (/speckit-plan command output)
├── research.md          # Phase 0 output (/speckit-plan command)
├── data-model.md        # Phase 1 output (/speckit-plan command)
├── quickstart.md        # Phase 1 output (/speckit-plan command)
└── tasks.md             # Phase 2 output (/speckit-tasks command)
```

### Source Code (repository root)

```text
wowmenfashions/
├── Components/
│   └── Pages/
│       ├── Category.razor    # Reset state on parameters set
│       └── Cart.razor        # New cart page implementation
├── Services/
│   └── ProductService.cs     # Inject IMemoryCache and implement caching
└── Program.cs                # Register AddMemoryCache()
```

**Structure Decision**: Standard Blazor Server project structure. Modifying existing services and adding a new page component.

## Complexity Tracking

> **Fill ONLY if Constitution Check has violations that must be justified**

No violations.
