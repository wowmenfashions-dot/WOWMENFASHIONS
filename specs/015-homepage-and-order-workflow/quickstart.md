# Quickstart & Validation

1. **Database Update**: Execute `005_OrderWorkflow.sql` in SQL Server Management Studio to add tracking columns to `Orders` and update stored procedures.
2. **Run App**: Launch the app with `dotnet run`.
3. **Validate Homepage**: Visit `/` and verify the new banner (RFID, Zip Around Wallet) and product grids are present.
4. **Validate Product Details**: Visit a product page (e.g. `/product/1`), hover over the image to ensure it zooms, and verify colors display as squares instead of chips.
5. **Validate Footer**: Scroll to the bottom of the page and verify the YKK/LWG graphics and dark footer UI are visible.
6. **Validate Order Tracking**: Log in, place an order, and view the Order History page. Verify the status is "Placed". Use direct DB modification to update `TrackingUrl` and verify the tracking link appears in the UI.
