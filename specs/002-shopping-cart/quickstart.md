# Quickstart: Validation Guide

This guide details how to validate the Stage 2 (Shopping Cart) functionality once implementation is complete.

## Setup

1. Build the application: `dotnet build`
2. Run database migration scripts for the Cart tables and Stored Procedures (located in `wowmenfashions/Database/`).
3. Start the application: `dotnet run`

## Validation Scenarios

### 1. Adding an Item as a Guest
- **Action**: Navigate to `/products` and click "Add to Cart" on a product.
- **Expected**: The cart counter in the top navigation updates from 0 to 1. No login prompt should appear.

### 2. View Cart and Totals
- **Action**: Navigate to `/cart`.
- **Expected**: The added product is visible. The item total (Price * Quantity) and Subtotal are correctly calculated.

### 3. Modify Quantity & Inventory Bounds
- **Action**: On `/cart`, change the quantity. Try entering a number higher than available stock.
- **Expected**: A valid quantity updates the total immediately. An invalid quantity displays an error message and reverts the input.

### 4. Session Persistence
- **Action**: Refresh the `/cart` page or open it in a new tab within the same browser.
- **Expected**: The cart contents remain intact due to the guest cookie.

### 5. Checkout Enforcement
- **Action**: Click "Proceed to Checkout" on `/cart`.
- **Expected**: The browser redirects to `/login` (or `/register`) enforcing authentication.
