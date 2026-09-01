# Implementation Tasks: Exception Logging and UI Tweaks

## Phase 1: Database & Services
- [x] T001 Create and execute `CreateErrorLogsTable.sql` to add the `ErrorLogs` table to the database.
- [x] T002 Create `IErrorLoggerService.cs` and `ErrorLoggerService.cs` in `Services/` to insert exceptions into `ErrorLogs` using Dapper.
- [x] T003 Register `IErrorLoggerService` in `Program.cs`.

## Phase 2: UI & Exception Handling
- [x] T004 Update `wowmenfashions/Components/Layout/MainLayout.razor` to include an `<ErrorBoundary>` that catches exceptions and logs them via `IErrorLoggerService`.
- [x] T005 Update `MainLayout.razor`'s top navigation bar to display the logged-in user's initials using `AuthenticationStateProvider`.
- [x] T006 Update `MainLayout.razor`'s `MudThemeProvider` to replace the black-and-white theme with the elite Navy Blue (`#0B192C`) and Gold (`#D4AF37`) palette.

## Phase 3: Validation
- [x] T007 Restart application and manually trigger an error to verify logging, then test the new UI theme and initials display.
