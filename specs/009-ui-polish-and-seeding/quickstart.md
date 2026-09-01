# Quickstart Validation: UI Polish and Database Seeding Expansion

## Prerequisites
- The SQL Server must be running.
- You must execute `Database/02_SeedDummyData.sql` against the database via SSMS or Azure Data Studio.

## Validation Scenarios

### Scenario 1: SQL Data Validation
1. Open your SQL client.
2. Run `SELECT COUNT(*) FROM Categories;` (should be at least 6).
3. Run `SELECT COUNT(*) FROM Products;` (should be at least 400).
4. Run `SELECT TOP 1 ImageUrl FROM Products ORDER BY Id DESC;` (Verify it ends in `.avif`).

### Scenario 2: Visual Uniformity Validation
1. Start the Blazor application (`dotnet run`).
2. Navigate to `/category/mens`.
3. Verify that the grid displays 4 cards per row (on large screens) and that **every single card in a row is the exact same height**, regardless of whether the image loads successfully or fails.
4. Verify the "Add" button is pinned to the absolute bottom of every card.
5. Verify the Product Title typography is larger and easier to read.

### Scenario 3: Category Filtering Validation
1. Open the Category dropdown filter.
2. Verify you see "Kids", "Toddler", "Adults", and "Teenager" alongside the originals.
3. Select "Kids" and click "Apply".
4. Verify exactly 50 products are loaded for the Kids category.
