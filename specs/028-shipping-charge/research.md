# Research

## Settings Storage
- **Decision**: Create a single-row `StoreSettings` table.
- **Rationale**: Currently, there is no generic site-wide settings table. Creating one will allow storing `FreeShippingThreshold` and `ShippingChargeAmount`, and can be expanded in the future for other settings.
- **Alternatives considered**: Appsettings.json - rejected because admin users need to change this easily from the dashboard without redeploying or accessing the server.

## Default Values
- **Decision**: 1000 INR for Threshold, 100 INR for Shipping Charge.
- **Rationale**: Derived from the user requirements and clarification phase.
