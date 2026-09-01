# Research: Swap BCrypt for Microsoft PasswordHasher

## Decision: PasswordHasher Implementation
**Decision**: We will instantiate a `PasswordHasher<string>` (where the generic type is just a string or a dummy user object, since we don't use full ASP.NET Identity users) inside our custom `wowmenfashions.Services.PasswordHasher`.
**Rationale**: `Microsoft.AspNetCore.Identity.PasswordHasher<TUser>` requires a generic type parameter. Passing a dummy `string` is a standard workaround when you just want to use the hashing utility independently of the full EF Core Identity framework.
**Alternatives considered**: Setting up full ASP.NET Core Identity (overkill, violates current architecture). Using `Rfc2898DeriveBytes` manually (too complex, reinventing the wheel).

## Decision: Removing the Nuget Package
**Decision**: Use `dotnet remove package BCrypt.Net-Next` (or manually edit `.csproj`) to strictly adhere to the user's preference of removing the 3rd-party dependency entirely.
