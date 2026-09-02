# Data Model

## StoreSettings (New Table)
Represents global application configuration settings.

**Fields**:
- `Id` (INT, Primary Key) - Always 1 to ensure a single row of settings.
- `FreeShippingThreshold` (DECIMAL(18,2)) - The minimum cart subtotal required to avoid a shipping charge.
- `ShippingChargeAmount` (DECIMAL(18,2)) - The fixed amount charged for shipping if the cart total is below the threshold.

## Stored Procedures
- `dbo.StoreSettings_Get`: Retrieves the single row of settings.
- `dbo.StoreSettings_Update`: Updates the settings row.
