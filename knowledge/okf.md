# Open Knowledge Format (OKF) Index

This directory contains the Open Knowledge Format (OKF) files for the WOWMENFASHIONS project.
All agents and developers MUST consult these files before making architectural changes, modifying existing workflows, or introducing new patterns.

## Project Guidelines
- **Architecture**: 
  - **Background Jobs**: Use `Hangfire` for non-blocking asynchronous tasks (e.g., email dispatch, cart abandonment).
  - **Email Notifications**: Use Azure Communication Services (ACS). All email dispatch must be non-blocking (via Hangfire) and any exceptions should be caught and logged to `EmailNotificationLogs` (never re-thrown to the UI).
- **Database**: SQL Server schemas and stored procedures.
- **Frontend**: Blazor Web App (wowmenfashions).

## Rules for Agents
1. Always check this file and related OKF files before starting a task.
2. Ensure any new knowledge or decisions are documented here in the OKF directory.
3. Follow the project's SpecKit workflow as defined in `.agents/AGENTS.md`.
