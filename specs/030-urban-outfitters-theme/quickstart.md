# Quickstart Validation Guide: Theme Overhaul

## 1. Prerequisites
- The application must be running locally via `run.ps1` or `dotnet run`.
- No database migrations are required for this UI change.

## 2. Validation Scenarios

### Scenario 1: Homepage Visual Check
1. Navigate to `/` (Homepage).
2. **Verify**: The global background color should be white/off-white depending on the palette selection.
3. **Verify**: Typography should reflect the updated Google Fonts (e.g., `Outfit`). Headings should have specific weights.

### Scenario 2: Product Card Minimalist Design
1. Navigate to the Catalog or view the products on the Homepage.
2. **Verify**: Product cards should NOT have any drop shadows.
3. **Verify**: Product images should be flush with the edges of the card.
4. **Verify**: Text beneath the product image (Title, Price) should be left-aligned and stark black.

### Scenario 3: Button and Input Hover States
1. Navigate to a product detail page or the Checkout page.
2. **Verify**: Primary buttons should be flat (no shadow) and stark black.
3. **Verify**: Hovering over a primary button should change the background to a dark gray.
4. **Verify**: Text fields should have sharp (or minimally rounded) borders.

## 3. Expected Outcome
The aesthetic of the application should feel noticeably flatter, highly typographic, and starkly contrasted, mirroring the minimalist retail experience of the reference design.
