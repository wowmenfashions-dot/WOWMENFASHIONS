# Quickstart & Validation: Stage 3

This document defines how to manually validate the Custom Authentication feature.

## Prerequisites
1. Ensure the `wowmenfashions` application is running (`dotnet run`).
2. Ensure the SQL database is running and `003_CustomerTables.sql` along with the SPs are executed.

## Scenario 1: User Registration
1. Navigate to `/register`.
2. Fill out the form with a new email and password.
3. Submit the form.
4. **Expected**: Account is created, user is redirected to `/login`, and a success message is displayed.

## Scenario 2: Login and Session
1. Navigate to `/login`.
2. Enter the credentials from Scenario 1.
3. **Expected**: User is authenticated, redirected to `/` or `/account/profile`. The navigation bar should update to show user-specific links (e.g., "Profile", "Logout") instead of "Login".

## Scenario 3: Profile Protection
1. Open a new incognito window (Guest).
2. Attempt to navigate directly to `/account/profile` or `/checkout`.
3. **Expected**: User is immediately redirected to `/login`.

## Scenario 4: Logout
1. While logged in, click "Logout".
2. **Expected**: Session is destroyed, UI reverts to Guest state, and navigating back to `/account/profile` redirects to `/login`.
