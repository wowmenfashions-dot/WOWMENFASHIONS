# Validation Guide: Checkout System

## Prerequisites
- Web Application running on `http://localhost:5124`
- Database schema updated with the new `Order` and `OrderItem` tables (via `Database/04_CheckoutSystem.sql`)

## Scenario 1: Verify Checkout Calculation
1. Open the application in your browser.
2. Add any product to the cart.
3. Click "Checkout" in the Cart sidebar or navigate to `/checkout`.
4. **Expected Outcome**:
   - The Order Summary shows the correct product price.
   - The Shipping fee is applied (e.g., $10 flat rate).
   - The GST is calculated correctly (e.g., 10% of subtotal).
   - The Total accurately sums these values.

## Scenario 2: Apply a Coupon
1. While on the checkout page, locate the Coupon input field.
2. Enter the valid dummy code `DISCOUNT10` and apply it.
3. **Expected Outcome**:
   - The subtotal displays a 10% discount subtracted from it.
   - The Total amount is updated instantly and correctly reflects the new discounted subtotal.

## Scenario 3: Dummy Payment & Order Placement
1. Fill out the mock shipping/billing address form on the Checkout page.
2. Click the "Place Order" button.
3. **Expected Outcome**:
   - The button shows a loading spinner ("Processing Payment...").
   - After a ~2 second simulated delay, the payment succeeds.
   - You are redirected to a success/confirmation page.
   - Opening the cart again shows that it has been cleared (0 items).

## Scenario 4: Placeholder Image Fallback
1. (Developer step) Use the Admin Dashboard or SQL to create a product with an empty or null `ImageUrl`.
2. Add this specific product to your cart.
3. Navigate to the Checkout page.
4. **Expected Outcome**:
   - The product row in the Order Summary displays a clean placeholder `dummy-product.avif` instead of a broken image link.
