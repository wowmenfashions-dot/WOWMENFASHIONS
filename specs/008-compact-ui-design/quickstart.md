# Quickstart Validation: Compact UI Redesign

## Prerequisites
- The application must be running (`dotnet run`).
- You must have a screen resolution typical of a standard laptop (1080p).

## Validation Scenarios

### Scenario 1: Layout & Navigation
1. Open the application.
2. Verify the AppBar is slimmer.
3. Verify the sidebar navigation links are closer together (Dense="true" on NavMenu).

### Scenario 2: Category Filters & Grid
1. Navigate to `/category/mens`.
2. Verify the filters at the top are very compact (Dense textfields and selects).
3. Verify the product grid has very small gaps (`Spacing="1"`).
4. Verify you can see at least 2 full rows of products *plus* the filters without scrolling.

### Scenario 3: Product Details
1. Click on a product.
2. Verify the hero image is smaller (max height constrained).
3. Verify the title, price, and description have very tight margins.
4. Verify the entire details page fits on a single screen without scrolling.

### Scenario 4: Cart
1. Add an item and go to `/cart`.
2. Verify the MudTable uses `Dense="true"`.
3. Verify the order summary box is compact and fits on the same screen.
