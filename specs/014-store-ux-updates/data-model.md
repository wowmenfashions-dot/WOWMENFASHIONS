# Data Model: Store UX Updates

## 1. CustomerAddresses

**Fields**:
- `Id` (INT, Primary Key)
- `CustomerId` (INT, Foreign Key -> Customers)
- `FullName` (NVARCHAR(100))
- `AddressLine` (NVARCHAR(200))
- `City` (NVARCHAR(100))
- `PostalCode` (NVARCHAR(20))
- `Country` (NVARCHAR(100))
- `IsDefaultShipping` (BIT)
- `IsDefaultBilling` (BIT)

## 2. Products (Updates)

**New Fields**:
- `AvailableColors` (NVARCHAR(MAX) or NVARCHAR(500)) - Comma-separated list of colors.

## 3. CartItems (Updates)

**New Fields**:
- `SelectedColor` (NVARCHAR(50)) - The color selected by the user when adding to cart.

## 4. OrderItems (Updates)

**New Fields**:
- `SelectedColor` (NVARCHAR(50)) - The color selected for the order item.
