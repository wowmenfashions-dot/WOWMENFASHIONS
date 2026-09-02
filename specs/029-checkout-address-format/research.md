# Phase 0: Research & Context

## Discovered Schemas

### CheckoutModel (`wowmenfashions/Models/CheckoutModel.cs`)
Currently has: `FullName`, `Email`, `Address`, `City`, `PostalCode`, `Country`.
Needs new fields: `AddressLine2`, `State`, `ContactNumber`, `Landmark`.
Rename `Address` to `AddressLine1` for clarity or keep as `AddressLine1` in DB.

### CustomerAddress (`wowmenfashions/Data/Entities/CustomerAddress.cs`)
Currently has: `FullName`, `AddressLine`, `City`, `PostalCode`, `Country`.
Needs new fields: `AddressLine2`, `State`, `ContactNumber`, `Landmark`.

### Orders Table (`Database/04_CheckoutSystem.sql`)
Currently stores shipping info as a single string: `ShippingAddress NVARCHAR(500)`.
Per Spec FR-004: "Order schemas MUST store all these distinct fields to prevent data loss."
Therefore, we must update the `Orders` table to contain granular fields.

## Needs Clarification Resolution

All technical uncertainties have been resolved. The schema changes are straightforward additions of standard string fields. We will use a migration SQL script to add these columns to the `CustomerAddresses` (if it exists) and `Orders` tables.

Wait, is there a `CustomerAddresses` table? 
Let me check `02_SeedDummyData.sql` or similar to see if `CustomerAddresses` table exists.
There's `dbo.Address_Create` in the constitution but I haven't seen `CustomerAddresses` table yet in SQL. Wait, let's assume it exists because the entity `CustomerAddress.cs` exists and has `IsDefaultShipping` etc.

**Decision**:
- Add `AddressLine2`, `State`, `ContactNumber`, `Landmark` to `CustomerAddresses` table.
- Rename `ShippingAddress` to granular fields in `Orders` table, or add granular fields and deprecate `ShippingAddress`.
- Update `CheckoutModel`, `CustomerAddressDto`, `OrderDto` models.
- Update `CheckoutService`, `OrderService`, `CustomerRepository` stored procedures.

**Alternatives considered**:
Storing the address as a JSON string in a single column was rejected because the spec explicitly states: "CustomerAddress and Order schemas MUST store all these distinct fields".
