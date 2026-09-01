# Quickstart: UI Theme Enhancements Validation

This guide explains how to validate the UI Theme and Layout Enhancements.

## Prerequisites
- WOWMENFASHIONS web application must be running (`.\run.ps1`).
- The application must be accessible via `http://localhost:5124`.

## Validation Scenarios

### 1. Global Theme Verification
**Action**: Open the application home page.
**Expected**: The global color palette is updated (e.g., Deep Navy and Gold Accent instead of default MudBlazor purple). The background and app bar colors reflect the new premium theme.

### 2. Cart Badge Fix
**Action**: Ensure there is at least 1 item in the cart. Look at the cart icon in the top right app bar.
**Expected**: The red badge containing the number of items is completely visible and not clipped.

### 3. Category Header Spacing & Filters
**Action**: Navigate to any category (e.g., Men's).
**Expected**:
- The excessive white space above the breadcrumbs and filters is gone.
- The filter dropdowns (Category, Subcategory, Price, Color) are aligned horizontally on a single line on desktop screens.

### 4. Product Grid & Fallback Image
**Action**: Scroll down to view the product grid on the category page.
**Expected**:
- The grid items have visible spacing between them.
- The "Sale!" badge renders perfectly overlapping the top-left corner of the product images.
- (Optional Dev Test) If you manually change a product's ImageUrl in the database to an invalid URL, the product card gracefully falls back to a placeholder image or a clean grey box rather than a broken browser image icon.
