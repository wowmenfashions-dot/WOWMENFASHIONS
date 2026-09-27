# Quickstart Validation Guide

## Prerequisites
- The application must be running locally.
- Test data (products, categories, AVIF images) must exist in the database.

## Validation Scenarios

### 1. Validate Navigation Menu
1. Start the application and open a browser to `http://localhost:5124`.
2. **Verify**: The main navigation menu is collapsed (not visible).
3. **Verify**: The hamburger menu toggle icon is distinctly red.
4. Click the hamburger icon.
5. **Verify**: The navigation menu expands/becomes visible.

### 2. Validate Product Details UI
1. Navigate to a product page (e.g., `http://localhost:5124/product/1`).
2. **Verify**: The layout matches the provided `refeence1.jpg` design.
3. Select a color swatch.
4. **Verify**: The selected swatch is visually highlighted according to `refeence2.jpg`.
5. Click the zoom icon on the main product image.
6. **Verify**: A zoomed-in view (modal or overlay) opens, matching the behavior in `refence3_zoom.jpg`.

### 3. Validate Category Layout
1. Navigate to the Men's category page (e.g., `http://localhost:5124/category/mens`).
2. **Verify**: The page layout and promotional imagery align with `reference4_jpg.jpg`.
3. **Verify**: The images are served in `.avif` format (inspect element -> Network tab).
