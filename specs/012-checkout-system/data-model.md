# Data Model: Checkout System

## Entities

### `Order`
Represents a finalized customer purchase.
- **Id**: `int` (Primary Key, Identity)
- **OrderDate**: `datetime`
- **Subtotal**: `decimal(18,2)`
- **ShippingFee**: `decimal(18,2)`
- **TaxAmount**: `decimal(18,2)`
- **DiscountAmount**: `decimal(18,2)`
- **TotalAmount**: `decimal(18,2)`
- **Status**: `nvarchar(50)` (e.g., 'PaymentCompleted', 'Processing')
- **CustomerName**: `nvarchar(100)`
- **CustomerEmail**: `nvarchar(100)`
- **ShippingAddress**: `nvarchar(500)`

### `OrderItem`
Represents the individual products inside an order.
- **Id**: `int` (Primary Key, Identity)
- **OrderId**: `int` (Foreign Key -> Order.Id)
- **ProductId**: `int`
- **ProductName**: `nvarchar(255)`
- **Price**: `decimal(18,2)`
- **Quantity**: `int`

### `Coupon`
Represents available discounts. (Can be static in code or DB table for MVP).
- **Code**: `nvarchar(50)` (Primary Key)
- **DiscountPercentage**: `decimal(5,2)` (e.g., 10.00 for 10%)

## Validation Rules

- **Coupon**: Must match a known code string to be applied.
- **Totals**: `TotalAmount` must equal `(Subtotal - DiscountAmount) + ShippingFee + TaxAmount`.
- **Order**: Needs at least 1 `OrderItem` to be valid.

## State Transitions
- `Cart` (in session) -> User initiates checkout -> Enters Details -> Clicks "Pay" -> Simulated Delay -> Creates `Order` with status `PaymentCompleted` -> Clears `Cart`.
