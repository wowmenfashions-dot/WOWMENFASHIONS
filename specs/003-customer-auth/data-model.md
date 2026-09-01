# Data Model: Customer

## Entities

### Customer

```csharp
public class Customer
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? Phone { get; set; }
    public string PasswordHash { get; set; } = string.Empty;
    public bool IsEmailVerified { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime? LastLoginAt { get; set; }
}
```

## SQL Schema

```sql
CREATE TABLE [dbo].[Customers] (
    [Id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [FirstName] NVARCHAR(100) NOT NULL,
    [LastName] NVARCHAR(100) NOT NULL,
    [Email] NVARCHAR(256) NOT NULL,
    [Phone] NVARCHAR(20) NULL,
    [PasswordHash] NVARCHAR(MAX) NOT NULL,
    [IsEmailVerified] BIT NOT NULL DEFAULT 0,
    [IsActive] BIT NOT NULL DEFAULT 1,
    [CreatedAt] DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
    [UpdatedAt] DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
    [LastLoginAt] DATETIME2 NULL
);

CREATE UNIQUE NONCLUSTERED INDEX [IX_Customers_Email] 
ON [dbo].[Customers] ([Email]);
```

## Stored Procedures

1. **`dbo.Customer_Create`**: Inserts a new customer (validating unique email) and returns the ID.
2. **`dbo.Customer_GetByEmail`**: Retrieves a customer record for login verification.
3. **`dbo.Customer_GetById`**: Retrieves a customer profile.
4. **`dbo.Customer_UpdateLastLogin`**: Updates the `LastLoginAt` timestamp.
