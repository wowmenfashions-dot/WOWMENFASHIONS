# Data Model: SEO and UI Enhancements

*No new database tables are required for this feature.*

## Existing Entity Usage
- **Product**: Used by the `SitemapController` to generate dynamic `<url>` entries for each active product page.
- **Category**: Used by the `SitemapController` to generate dynamic `<url>` entries for category pages.

## Sitemap XML Schema
The generated `sitemap.xml` will follow the standard `sitemaps.org` schema:
```xml
<?xml version="1.0" encoding="UTF-8"?>
<urlset xmlns="http://www.sitemaps.org/schemas/sitemap/0.9">
  <url>
    <loc>https://wowmenfashions.com/</loc>
    <changefreq>daily</changefreq>
    <priority>1.0</priority>
  </url>
  <!-- Dynamically generated product URLs -->
</urlset>
```
