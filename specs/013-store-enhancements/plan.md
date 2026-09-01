# Implementation Plan: 013-store-enhancements

## Technical Context

The application is a Blazor Server app utilizing MudBlazor and Dapper.
- **Currency**: We can configure the `CultureInfo` globally in `Program.cs`.
- **Auto-fill Shipping**: We can inject `ICustomerService` into `Checkout.razor` to retrieve the current user's profile based on their Auth state (using `AuthenticationStateProvider`).
- **Order Summary**: We need to update the mathematical logic in `Checkout.razor` to include `TaxAmount` (10%) and apply any discounts.
- **Categories**: The database is missing `Kids` and `Toddler` categories. We need to add them to the `Categories` table via a SQL script.

## Proposed Changes

### Database Layer
#### [NEW] `Database\05_AddCategories.sql`
- Insert `Kids` (Slug: `kids`) and `Toddler` (Slug: `toddler`) into the `Categories` table.

### Backend Layer
#### [MODIFY] `wowmenfashions\Program.cs`
- Set `CultureInfo.DefaultThreadCurrentCulture` and `CultureInfo.DefaultThreadCurrentUICulture` to `en-IN`.
- Add `app.UseRequestLocalization()` with `en-IN` as the default.

### Frontend Layer
#### [MODIFY] `wowmenfashions\Components\Pages\Checkout.razor`
- Inject `AuthenticationStateProvider` and `ICustomerService` to retrieve logged-in user details.
- In `OnInitializedAsync`, check if the user is authenticated. If so, fetch customer by username and map Address, City, ZipCode, State, Phone to `CheckoutModel`.
- Update `TaxAmount` calculation to be 10% of `Subtotal`.
- Update `TotalAmount` calculation to be `Subtotal + ShippingFee + TaxAmount - DiscountAmount`.

## Verification Plan

### Automated Tests
- N/A

### Manual Verification
- Navigate to the site and confirm prices are in ₹ (INR).
- Log in and go to checkout, verifying the fields are auto-filled.
- Add an item to cart and verify the tax calculation in the order summary is 10% and added to the total.
- Verify "Kids" and "Toddlers" appear in the Categories dropdown/nav if populated, or can be fetched from the DB.
