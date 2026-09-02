# Feature Specification: SEO and UI Enhancements

**Feature Branch**: `[023-seo-and-ui-enhancements]`

**Created**: 2026-09-01

**Status**: Draft

**Input**: User description: "1] Highly optimised SOE 2] Should works on any corporate environment 3] Google search indexing 4] create site maps and robo.txt etc.. required for make this page very cisible on the google search or any search 5] analyse the view source and utilize anything related. 6] make sure all the pages are visible in the google search... 7] i want this logo to be on the page browser tab + page / application logo. 8] align the count of items in the cart 9] can you make sure this content is below the coursel. screenshot attached. garentee, delivery, rfid. 10 ]YKK, leateher froup etc image. can you recreate it and put it in the home page."

## User Scenarios & Testing *(mandatory)*

### User Story 1 - Search Engine Visibility (Priority: P1)

As a potential customer, I want to find the WOWMENFASHIONS website easily via Google Search so that I can browse and purchase products.

**Why this priority**: Discoverability is critical for e-commerce. If the site is not indexed properly, traffic and sales will be low.

**Independent Test**: Can be tested independently by verifying the presence of `robots.txt`, `sitemap.xml`, and appropriate meta tags in the page headers.

**Acceptance Scenarios**:

1. **Given** a web crawler accesses the site, **When** it requests `robots.txt` and `sitemap.xml`, **Then** valid files are returned allowing indexing of the main pages.
2. **Given** a user views the homepage, **When** they inspect the page source, **Then** they see relevant SEO meta tags (title, description, keywords).

---

### User Story 2 - Brand Identity & Logo (Priority: P2)

As a user, I want to see the brand's logo in the browser tab and on the navigation bar so that I know I am on the correct, trusted website.

**Why this priority**: Brand identity builds trust and provides a professional appearance.

**Independent Test**: Can be fully tested by loading the site and visually confirming the favicon and app bar logo.

**Acceptance Scenarios**:

1. **Given** a user opens the website, **When** the page loads, **Then** the browser tab displays the WOW-LOGO as the favicon.
2. **Given** a user navigates the site, **When** they look at the top navigation bar, **Then** the WOW-LOGO is displayed instead of plain text.

---

### User Story 3 - UI Polish & Trust Badges (Priority: P2)

As a shopper on the homepage, I want to immediately see trust badges (Guarantee, Free Delivery, RFID Protection, YKK Zippers, LWG) below the main carousel so that I feel confident in the product quality.
I also want the cart item count to align perfectly with the cart icon so the site looks professionally designed.

**Why this priority**: Trust signals and polished UI (cart alignment) directly impact conversion rates.

**Independent Test**: Can be visually tested on the homepage and by adding an item to the cart.

**Acceptance Scenarios**:

1. **Given** a user lands on the homepage, **When** they scroll past the main carousel, **Then** they see banners for "1 Year Guarantee, Free Delivery, RFID Protected" and "YKK, LWG".
2. **Given** a user has items in their cart, **When** they look at the cart icon in the navigation bar, **Then** the red item count badge is perfectly aligned with the cart icon.

## Requirements *(mandatory)*

### Functional Requirements

- **FR-001**: System MUST include a static or dynamically generated `robots.txt` in the root directory that allows search engine crawlers.
- **FR-002**: System MUST include a `sitemap.xml` listing core URLs (Home, Categories, Products).
- **FR-003**: System MUST render SEO meta tags (title, description) in the HTML `<head>`.
- **FR-004**: System MUST function in corporate environments (e.g., standard HTTP/HTTPS ports, handling WebSocket restrictions via Long Polling fallback for Blazor Server).
- **FR-005**: System MUST serve the custom WOW-LOGO as the site favicon.
- **FR-006**: System MUST display the WOW-LOGO image in the `MainLayout` navigation bar.
- **FR-007**: System MUST display trust badges/banners immediately below the homepage carousel.
- **FR-008**: System MUST display the cart item count badge aligned correctly with the cart icon.

## Success Criteria *(mandatory)*

### Measurable Outcomes

- **SC-001**: 100% of core pages (Home, Category, Product) are accessible to web crawlers and listed in `sitemap.xml`.
- **SC-002**: Visual inspection confirms the logo is present in the navigation bar and as the favicon on all pages.
- **SC-003**: The homepage UI structural changes (trust banners below carousel) are visible without breaking responsiveness.
- **SC-004**: Cart badge overlaps the cart icon precisely based on visual standards.

## Assumptions

- We assume the provided logo URL is the exact image to be used for both the page logo and favicon.
- We assume the trust banners should be implemented using responsive HTML/CSS and icons (MudBlazor) rather than single large static images, to ensure better quality and SEO.
- We assume standard Blazor Server hosting on port 80/443 is sufficient for "corporate environment" compatibility since no non-standard ports are used.
