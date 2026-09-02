# Research: SEO and UI Enhancements in Blazor Server

## Blazor Server SEO Meta Tags
- **Decision**: Use Blazor's `<PageTitle>` and `<HeadContent>` components in Razor pages (e.g., `Home.razor`) instead of hardcoding in `App.razor`.
- **Rationale**: Allows dynamic SEO tags per page, ensuring accurate indexing for Product and Category pages.
- **Alternatives**: Static meta tags in `App.razor` (rejected because it prevents dynamic indexing of products).

## Sitemap Generation
- **Decision**: Implement a lightweight MVC `SitemapController` returning `application/xml`.
- **Rationale**: Blazor Server routing does not easily serve raw XML endpoints from Razor components. An MVC controller mapped via `app.MapControllers()` is the standard ASP.NET Core approach.
- **Alternatives**: Static `sitemap.xml` in `wwwroot` (rejected because products are dynamic and added frequently).

## Corporate Environment Support
- **Decision**: Rely on default ASP.NET Core SignalR negotiation (WebSockets -> Server-Sent Events -> Long Polling).
- **Rationale**: Blazor Server automatically falls back to Long Polling if WebSockets are blocked by corporate proxies. No specific code changes are required other than ensuring no manual WebSocket overrides exist.

## Trust Badges UI Implementation
- **Decision**: Use `MudGrid` and `MudPaper` with MudBlazor icons instead of large static images.
- **Rationale**: Better responsive behavior, faster load times (better SEO), and accessibility compared to static images.
