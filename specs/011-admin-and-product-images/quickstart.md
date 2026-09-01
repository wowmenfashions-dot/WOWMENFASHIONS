# Quickstart: Admin Panel, Multiple Images, & Pagination Validation

This guide explains how to validate the newly added features.

## Prerequisites
- The database must be seeded with the new `03_AdminAndImages.sql` script.
- WOWMENFASHIONS web application must be running.

## Validation Scenarios

### 1. Title Rebranding
**Action**: Open the application home page `http://localhost:5124`.
**Expected**: The browser tab title and the top-left App bar text both display "WOWMEN".

### 2. Admin Authentication
**Action**: Try to navigate directly to `http://localhost:5124/admin`.
**Expected**: You should be automatically redirected to the `/login` page.
**Action**: Enter username `admin` and password `password`.
**Expected**: You are successfully logged in and redirected back to the `/admin` dashboard.

### 3. Product Multiple Images
**Action**: Navigate to any product details page (e.g., click on a product from the home page).
**Expected**: You should see a MudCarousel or a gallery view showing the primary image along with additional images seeded from the database. You can navigate between the different images.

### 4. Category Pagination
**Action**: Navigate to `http://localhost:5124/category/mens`.
**Expected**: Only the first 20 products are displayed. At the bottom of the grid, a pagination control is visible.
**Action**: Click on page "2".
**Expected**: The next 20 products load correctly.
