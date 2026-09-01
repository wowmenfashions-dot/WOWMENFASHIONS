# Implementation Plan: Homepage & Order Workflow Redesign

**Branch**: `[015-homepage-and-order-workflow]` | **Date**: 2026-08-31 | **Spec**: [specs/015-homepage-and-order-workflow/spec.md](file:///c:/MyDrive/ProjectDrive/WOWMENFASHIONS/specs/015-homepage-and-order-workflow/spec.md)

**Input**: Feature specification from `/specs/015-homepage-and-order-workflow/spec.md`

## Summary

This feature updates the Product Details UI with image zoom and color squares, overhauls the Order workflow with granular statuses and courier tracking, restyles the footer to build trust, and totally redesigns the homepage with promotional banners and product grids.

## Technical Context

**Language/Version**: C# 10 / .NET 8 (Blazor Server)

**Primary Dependencies**: MudBlazor, Dapper

**Storage**: Microsoft SQL Server

**Testing**: Manual Validation

**Target Platform**: Web Browsers (Responsive)

**Project Type**: Blazor Server Application

**Constraints**: Adhere strictly to the project constitution (Dapper ONLY, Stored Procs ONLY, NO EF Core). AVIF image format where images are newly sourced.

## Constitution Check

*GATE: Must pass before Phase 0 research. Re-check after Phase 1 design.*

- **Principle 1 & 2 & 9 & 24**: SQL Server + Dapper ONLY. No Entity Framework. (Pass - we will use raw stored procedures for order status and tracking).
- **Principle 3 & 4**: Stored Procedures Only. No inline SQL. (Pass - we will create/update `dbo.Order_UpdateStatus` and `dbo.Order_UpdateTracking`).
- **Principle 5 & 22**: Strict layered architecture. (Pass - we will use `OrderService` -> `OrderRepository`).
- **Principle 29**: AVIF image format only. (Pass - we will use `.avif` extensions for new promotional banner images).

## Project Structure

### Documentation (this feature)

```text
specs/015-homepage-and-order-workflow/
├── plan.md              # This file
├── research.md          # Phase 0 output
├── data-model.md        # Phase 1 output
└── quickstart.md        # Phase 1 output
```

### Source Code (repository root)

```text
wowmenfashions/
├── Components/
│   ├── Pages/
│   │   ├── Home.razor             # Complete redesign
│   │   ├── ProductDetails.razor   # Add image zoom and color boxes
│   │   └── Account/
│   │       └── Orders.razor       # Add workflow tracking
│   └── Layout/
│       └── MainLayout.razor       # Footer updates
├── Data/
│   └── Entities/
│       └── Order.cs               # Add Status workflow & Courier properties
├── Database/
│   ├── Tables/
│   │   └── 005_OrderWorkflow.sql  # Migration script for courier details
│   └── StoredProcedures/
│       └── dbo.Order_GetByEmail.sql # Add new columns to SELECT
├── Models/
│   └── OrderDto.cs                # Add courier fields
```

**Structure Decision**: The structure aligns with the Modular Monolith pattern specified in the constitution.
