# Quickstart: Deo Theme Validation

This guide explains how to validate the Deo eCommerce theme overhaul locally.

## 1. Prerequisites
- WOWMENFASHIONS repository cloned and dependencies restored.

## 2. Launching the App
1. Open a PowerShell terminal.
2. Run the application:
   ```powershell
   .\run.ps1
   ```
3. Open a browser to `http://localhost:5124`.

## 3. Validation Scenarios

### Scenario 1: Product Grid Styling
1. Navigate to the Homepage (`/`) or any Catalog category page.
2. Observe the Product Cards:
   - **Expected**: The product images should sit on a soft light gray (`#F3F4F6`) background.
   - **Expected**: The image container should have heavily rounded corners (approx `16px`).
   - **Expected**: There should be NO borders and NO drop shadows on the card itself.
   - **Expected**: Product text (Title, Price, Category) should be strictly left-aligned underneath the image.

### Scenario 2: Button and Input Geometry
1. Click around the application (e.g., "Add to Cart" or navigating to the Cart).
2. Observe the primary buttons:
   - **Expected**: Buttons should be perfectly pill-shaped (fully rounded left and right edges).
   - **Expected**: The background color should be a soft charcoal black (`#1E1E1E`), not jet black (`#000000`).
   - **Expected**: Hover states should gently shift color without adding heavy drop-shadows.
3. Observe input fields (if applicable on checkout/contact pages):
   - **Expected**: Inputs should also share the fully rounded pill-shaped geometry.
