# Implementation Plan: Exception Logging and UI Tweaks

## Technical Context
- **Framework**: Blazor Server (.NET 8)
- **Database**: SQL Server via Dapper
- **UI Framework**: MudBlazor

## Constitution Check
No violations. The plan adheres to the existing architecture (Dapper + SQL Server) and standard Blazor practices.

## Phase 0: Research
- **Exception Handling**: Blazor Server requires handling exceptions in the ASP.NET Core pipeline (for initial load) and using `<ErrorBoundary>` for interactive component errors. We will implement an `IErrorLoggerService` injected into the `<ErrorBoundary>` in `MainLayout.razor`.
- **Database Logging**: We will create a new table `ErrorLogs` via a SQL script (`CreateErrorLogsTable.sql`) and use Dapper to insert records.
- **Initials**: We will inject `AuthenticationStateProvider` into the header component to extract the username/email and derive the initials.
- **Theme**: We will override the MudBlazor `PaletteLight` and `PaletteDark` in `MainLayout.razor` to use deep Navy Blue (`#0B192C`), rich Gold (`#D4AF37`), and premium grays, while removing the stark black/white defaults.

## Phase 1: Design & Contracts

### Data Model
**Table `ErrorLogs`**
- `Id` (INT IDENTITY PK)
- `Message` (NVARCHAR(MAX))
- `StackTrace` (NVARCHAR(MAX))
- `Timestamp` (DATETIME)
- `UserContext` (NVARCHAR(255))

### UI Updates
- `MainLayout.razor`: Update MudThemeProvider with new custom colors. Wrap `MudLayout` in `ErrorBoundary`. Update the top app bar to show a MudAvatar with the user's initials if logged in.
- `app.css`: Add any necessary overrides for the new elite color scheme (e.g., button hovers, rich background colors).

### Services
- `IErrorLoggerService` / `ErrorLoggerService`: Handles saving exceptions to the DB using Dapper.
