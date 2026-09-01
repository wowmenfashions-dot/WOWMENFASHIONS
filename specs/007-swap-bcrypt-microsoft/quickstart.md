# Quickstart Validation: Swap BCrypt for Microsoft PasswordHasher

## Prerequisites
- The application must be running (`dotnet run`).
- Any existing test users in the database will no longer be able to log in. You must create a new test user.

## Validation Scenarios

### Scenario 1: Clean Build
1. Run `dotnet build`.
2. Verify the project builds successfully with 0 errors and 0 warnings.
3. Check `wowmenfashions.csproj` to confirm `BCrypt.Net-Next` is completely removed.

### Scenario 2: Register & Login (E2E)
1. Navigate to `/register`.
2. Create a new account with a valid email and password.
3. Upon successful registration, the app should redirect to login.
4. Log in using the newly created credentials.
5. Verify you are successfully authenticated and redirected to the home page.
