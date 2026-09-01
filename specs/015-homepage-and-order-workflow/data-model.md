# Data Model: Homepage & Order Workflow

## Modified Entities

### Orders
Additional Columns:
- `CourierName` (NVARCHAR 100, NULL)
- `TrackingNumber` (NVARCHAR 100, NULL)
- `TrackingUrl` (NVARCHAR 500, NULL)

*Note*: `Status` is already present (NVARCHAR 50). We will enforce the new values conceptually: 'Placed', 'Processing', 'Pickup', 'In Transit', 'Delivered'.
