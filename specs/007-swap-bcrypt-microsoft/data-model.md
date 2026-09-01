# Data Model: Swap BCrypt for Microsoft PasswordHasher

## Relevant Existing Entities
- **CustomerDto / Users**: The `PasswordHash` column in the SQL database will now store Microsoft Identity v3 hashes instead of BCrypt hashes.

No schema changes are required because both hashes are just stored as strings (usually `NVARCHAR(MAX)` or `VARCHAR(255)`). Note that previous BCrypt hashes will fail verification with the new hasher, meaning any test accounts created prior to this feature will need to be re-created.
