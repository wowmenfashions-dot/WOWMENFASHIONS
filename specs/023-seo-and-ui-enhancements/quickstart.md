# Quickstart: Validating SEO and UI Enhancements

This guide explains how to verify the new SEO tags, dynamic sitemap, and UI changes locally.

## Prerequisites
- .NET 10 SDK installed.
- Local SQL Server running (or connection string configured).

## Run the Application
```bash
cd wowmenfashions
dotnet run
```

## Validation Scenarios

### 1. Verify UI and Logo
- Open `https://localhost:5001` (or the configured port).
- Check the browser tab for the new WOW-LOGO (favicon).
- Check the top-left App Bar for the WOW-LOGO image.
- Add an item to the cart and confirm the red badge overlaps the top-right of the cart icon accurately.
- Scroll below the carousel on the homepage and verify the new "Guarantee, Free Delivery, RFID" and "YKK/LWG" banners are displayed.

### 2. Verify SEO Meta Tags
- Right-click the homepage and select "View Page Source" (or use browser DevTools).
- Confirm the presence of `<title>`, `<meta name="description">`, and `<meta name="keywords">` within the `<head>` section.

### 3. Verify Sitemap and Robots.txt
- Navigate to `https://localhost:5001/robots.txt`. Ensure it `Allow: /`.
- Navigate to `https://localhost:5001/sitemap.xml`. Ensure it returns valid XML with URLs for the homepage, categories, and products.
