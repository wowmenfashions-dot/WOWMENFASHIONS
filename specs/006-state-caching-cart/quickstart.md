# Quickstart Validation: State, Caching, and Cart

## Prerequisites
- The application must be running (`dotnet run`).

## Validation Scenarios

### Scenario 1: State Reset on Navigation
1. Navigate to Men's category.
2. Set Price to max ₹1000 and Color to a specific color.
3. Click "Women's" in the navigation menu.
4. Verify the Category page reloads with the Max Price slider reset to ₹3000 and Color reset to "Any Color".

### Scenario 2: In-Memory Caching
1. Navigate to the Men's category (first load will query the database).
2. Navigate to Women's category, then back to Men's category.
3. Verify the second load of the Men's category is instantaneous and does not produce a SQL query log for fetching categories/products.

### Scenario 3: Functional Cart Page
1. Add a product to the cart by clicking "Add to Cart" on a product card.
2. Navigate to `http://localhost:5124/cart`.
3. Verify the page loads successfully (no 404) and displays the added item with its quantity and price.
4. Verify the layout adapts cleanly to mobile viewport sizes.
