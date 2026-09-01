# Implementation Plan: UI Polish and Database Seeding Expansion

**Branch**: `009-ui-polish-and-seeding` | **Date**: 2026-08-30 | **Spec**: [spec.md](file:///c:/MyDrive/ProjectDrive/WOWMENFASHIONS/specs/009-ui-polish-and-seeding/spec.md)

**Input**: Feature specification from `/specs/009-ui-polish-and-seeding/spec.md`

## Summary

This feature resolves critical visual defects in the product grid introduced during the recent compact redesign, ensuring that cards are of uniform height regardless of image state. Simultaneously, it generates a robust SQL data seeding script (`02_SeedDummyData.sql`) to inject 400 new products across 6 categories using `WHILE` loops, ensuring all dummy image URLs comply with the newly mandated `.avif` format.

## Technical Context

**Language/Version**: C#, .NET 10.0, Blazor Server, T-SQL

**Primary Dependencies**: `MudBlazor`, `Microsoft.Data.SqlClient`

**Storage**: SQL Server

**Testing**: Manual visual verification and SQL row-count queries.

**Target Platform**: Blazor Server Web Application

**Project Type**: Blazor Server Web Application

**Performance Goals**: Ensure the application UI can gracefully handle a dataset of 400+ products via existing paginated stored procedures.

**Constraints**:
- Must follow Constitution Principle 29 (AVIF image format).
- Must adhere to Principle 4 (No Direct SQL from application code). SQL seeding script must be executed manually against the DB.

## Constitution Check

*GATE: Must pass before Phase 0 research. Re-check after Phase 1 design.*

- **Principle 29 (AVIF Image Format Only)**: PASS - The SQL script will generate strings appending `.avif`.
- **Principle 4 (No Direct SQL)**: PASS - The SQL seeding is a separate database initialization script, not application runtime code.

## Project Structure

### Documentation (this feature)

```text
specs/009-ui-polish-and-seeding/
├── plan.md              # This file (/speckit-plan command output)
├── research.md          # Phase 0 output (/speckit-plan command)
├── data-model.md        # Phase 1 output (/speckit-plan command)
├── quickstart.md        # Phase 1 output (/speckit-plan command)
└── tasks.md             # Phase 2 output (/speckit-tasks command)
```

### Source Code (repository root)

```text
wowmenfashions/Components/Shared/
└── ProductCard.razor      # Apply flexbox column rules and button positioning

Database/
└── 02_SeedDummyData.sql   # New script with T-SQL WHILE loops to generate categories and products
```

## Complexity Tracking

> **Fill ONLY if Constitution Check has violations that must be justified**

No violations.
