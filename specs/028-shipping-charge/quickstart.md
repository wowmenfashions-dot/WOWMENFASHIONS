# Validation Quickstart

This guide explains how to validate that the shipping charge feature is working end-to-end.

## Prerequisites
- Application is running locally (`dotnet run`).
- Database is up to date with new stored procedures and the `StoreSettings` table.
- A user account to test checkout, and an admin account to configure settings.

## Scenarios

### Scenario 1: Verify Default Shipping Rules
1. Log in as a regular customer.
2. Add items to your cart such that the subtotal is **under 1000 INR**.
3. Proceed to the Cart/Checkout page.
4. **Expected Outcome**: A shipping charge of **100 INR** is displayed, and added to the total cost.
5. Add more items so the subtotal is **1000 INR or more**.
6. **Expected Outcome**: The shipping charge disappears or says "Free", and the total does not include the fee.

### Scenario 2: Admin Updates Configuration
1. Log in as an Admin.
2. Navigate to the Admin Dashboard and click on the "Store Settings" navigation link.
3. Update the **Free Shipping Threshold** to `500` INR and the **Shipping Charge** to `50` INR.
4. Save the settings.
5. **Expected Outcome**: A success message is displayed.

### Scenario 3: Verify Updated Rules
1. Using a customer session (or incognito window), add items totaling `700` INR to the cart.
2. Go to Checkout.
3. **Expected Outcome**: Shipping is Free, because the total (700) is greater than the new threshold (500).
4. Reduce the cart total to `400` INR.
5. **Expected Outcome**: A shipping charge of **50 INR** is applied.
