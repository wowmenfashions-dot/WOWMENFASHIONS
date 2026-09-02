# Phase 1: Data Model

## CheckoutModel (C#)
```csharp
public class CheckoutModel
{
    public string FullName { get; set; }
    public string Email { get; set; }
    public string AddressLine1 { get; set; }
    public string? AddressLine2 { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string Country { get; set; } = "India";
    public string PostalCode { get; set; }
    public string ContactNumber { get; set; }
    public string? Landmark { get; set; }
}
```

## Orders Table (SQL Server)
```sql
ALTER TABLE Orders
ADD 
    ShippingAddressLine1 NVARCHAR(500) NULL,
    ShippingAddressLine2 NVARCHAR(500) NULL,
    ShippingCity NVARCHAR(100) NULL,
    ShippingState NVARCHAR(100) NULL,
    ShippingCountry NVARCHAR(100) NULL,
    ShippingPostalCode NVARCHAR(50) NULL,
    ShippingContactNumber NVARCHAR(50) NULL,
    ShippingLandmark NVARCHAR(255) NULL;
```
*Note: To avoid breaking existing orders, we keep `ShippingAddress` (string) for legacy fallback or drop it after migrating data.*

## CustomerAddresses Table (SQL Server)
```sql
ALTER TABLE CustomerAddresses
ADD 
    AddressLine2 NVARCHAR(500) NULL,
    State NVARCHAR(100) NULL,
    ContactNumber NVARCHAR(50) NULL,
    Landmark NVARCHAR(255) NULL;
```

## CustomerAddress Entity (C#)
Add the properties above to `CustomerAddress.cs` and `CustomerAddressDto.cs`.
