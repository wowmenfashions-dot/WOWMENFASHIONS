# Implementation Plan: Swap BCrypt for Microsoft PasswordHasher

**Branch**: `007-swap-bcrypt-microsoft` | **Date**: 2026-08-30 | **Spec**: [spec.md](file:///c:/MyDrive/ProjectDrive/WOWMENFASHIONS/specs/007-swap-bcrypt-microsoft/spec.md)

**Input**: Feature specification from `/specs/007-swap-bcrypt-microsoft/spec.md`

## Summary

This feature replaces the 3rd-party `BCrypt.Net-Next` NuGet package with Microsoft's first-party `PasswordHasher<TUser>` from the `Microsoft.Extensions.Identity.Core` namespace (or equivalent) to fulfill the user's preference for Microsoft-provided components.

## Technical Context

**Language/Version**: C#, .NET 10.0

**Primary Dependencies**: `Microsoft.AspNetCore.Identity`, `Microsoft.Extensions.Identity.Core`

**Storage**: Existing SQL Server Database

**Testing**: Manual UI verification via quickstart.

**Target Platform**: Blazor Server

**Project Type**: Blazor Server Web Application

**Performance Goals**: N/A

**Constraints**: Must completely remove `BCrypt.Net-Next` from the `.csproj`. 

**Scale/Scope**: Very small. Touches `wowmenfashions.csproj` and `wowmenfashions/Services/PasswordHasher.cs`.

## Constitution Check

*GATE: Must pass before Phase 0 research. Re-check after Phase 1 design.*

- **Principle 5 (Isolated DB Access)**: PASS - No changes to data access.
- **Principle 23 (Technology Stack)**: PASS - Utilizing standard .NET Identity components.
- **Principle 28 (Error-Free Builds)**: PASS - Will ensure the project builds with 0 warnings.

## Project Structure

### Documentation (this feature)

```text
specs/007-swap-bcrypt-microsoft/
├── plan.md              # This file (/speckit-plan command output)
├── research.md          # Phase 0 output (/speckit-plan command)
├── data-model.md        # Phase 1 output (/speckit-plan command)
├── quickstart.md        # Phase 1 output (/speckit-plan command)
└── tasks.md             # Phase 2 output (/speckit-tasks command)
```

### Source Code (repository root)

```text
wowmenfashions/
├── wowmenfashions.csproj     # Remove BCrypt package
└── Services/
    └── PasswordHasher.cs     # Replace BCrypt.HashPassword with Microsoft PasswordHasher
```

**Structure Decision**: No new files. We are updating an existing abstraction (`PasswordHasher.cs`) to use a different underlying engine.

## Complexity Tracking

> **Fill ONLY if Constitution Check has violations that must be justified**

No violations.
