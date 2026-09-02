# Quickstart: Validation Guide

## Setup Prerequisites
Ensure that the SQL migrations to add new columns to the `Orders` and `CustomerAddresses` tables have been executed correctly against the local database.

## Test Scenarios

### 1. Checkout with new Address fields
1. Open the application and add an item to the cart.
2. Proceed to Checkout.
3. Observe the new address form containing Address 1, Address 2, City, State, Country (default India), Postal Code, Contact Number, and Landmark.
4. Leave Contact Number blank and try to place the order.
   - **Expected**: Validation error blocks the submission.
5. Fill in all details properly, including optional ones.
6. Place the order.
   - **Expected**: Order goes through successfully and redirects to confirmation or payment page.

### 2. Verify Order in Database
1. Run `sqlcmd -S (localdb)\MSSqlLocalDb -d wowmenfashions -U appuser -P Sandhya@143 -Q "SELECT TOP 1 ShippingState, ShippingContactNumber FROM Orders ORDER BY Id DESC"`
   - **Expected**: Data populated from checkout is correctly stored.

### 3. Verify Admin Dashboard
1. Log in as an Admin.
2. Go to the Order Details page.
   - **Expected**: The shipping address block displays all the newly added granular details (e.g., State, Contact Number).
