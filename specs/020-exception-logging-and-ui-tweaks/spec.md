# Exception Logging and UI Tweaks

## Problem Statement
The application currently lacks global exception handling and database logging, making it difficult to track down production issues. In addition, the UI needs personalization when users log in (displaying their initials in the top nav) and requires a more colorful, elite aesthetic to replace the stark black-and-white theme.

## User Scenarios & Testing

1. **Global Exception Logging**:
   - An unexpected error occurs on the server (e.g., a database timeout or null reference).
   - The user sees a friendly error page.
   - The exception details (message, stack trace, timestamp, user context) are automatically logged to a new database table.

2. **Personalized Navigation**:
   - A user named "John Doe" logs into the application.
   - The top navigation bar displays a circular avatar or text showing "JD" instead of the generic "Profile" or "Login" text.

3. **Elite Colorful Theme**:
   - The user visits the homepage and product pages.
   - The design incorporates a premium, rich color palette (moving away from purely black and white) while maintaining modern, clean typography and layout.

## Functional Requirements

- **Global Exception Handler**: Intercept unhandled exceptions globally.
- **Database Logging**: Create an `ErrorLogs` table in the database and write exceptions to it.
- **User Initials**: Extract the logged-in user's first and last name from their session/claims to generate and display initials in the top navigation bar.
- **Color Palette Update**: Update global CSS and theme configurations to introduce premium colors (e.g., navy blue, gold accents, or rich jewel tones) while maintaining the elite feel.

## Non-Functional Requirements

- **Performance**: Logging should be asynchronous to prevent slowing down request handling.
- **Aesthetics**: The new color theme must look highly premium and cohesive across all components.

## Assumptions
- The database schema can be safely migrated to include an `ErrorLogs` table.
- User accounts have first and last names (or at least a display name) to extract initials from.
- A global exception handler middleware or Blazor error boundary will be sufficient.

## Success Criteria
- 100% of unhandled server exceptions are recorded in the `ErrorLogs` table.
- Logged-in users see their initials in the navigation bar.
- The UI theme is no longer purely black and white, featuring a premium color palette.
