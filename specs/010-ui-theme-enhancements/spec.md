# Feature Specification: UI Theme & Layout Enhancements

**Feature Branch**: `010-ui-theme-enhancements`

**Created**: 2026-08-30

**Status**: Draft

**Input**: User description: "add some theme colors which fits the web application. too much space at top of category page. number not visible on cart icon. make filters align in single row, reduce font, mobile friendly. display sale icon on top of image. leave spacing between two images. put sample image for broken images."

## User Scenarios & Testing *(mandatory)*

### User Story 1 - Apply Global Theme & Colors (Priority: P1)

Users should see a cohesive, premium color theme applied across the entire web application (app bar, buttons, backgrounds) to make it look professional and appealing.

**Why this priority**: A professional theme establishes brand identity and improves user trust.

**Independent Test**: Load the application and verify that the global colors (primary, secondary, appbar) match a cohesive fashion-oriented theme.

**Acceptance Scenarios**:
1. **Given** the user visits any page, **When** the page loads, **Then** a defined custom `MudTheme` is applied.

---

### User Story 2 - Fix Header, Layout, and Filter Spacing (Priority: P1)

Users browsing categories should have a compact top header (no excessive whitespace), properly aligned category filters (single row, mobile-responsive), and visible cart item counts in the top navigation.

**Why this priority**: Poor spacing and broken alignments make the site difficult to use, especially on smaller screens.

**Independent Test**: Navigate to a category page on desktop and mobile viewports. The top whitespace should be minimal, filters should align nicely in a single row or wrap elegantly, and the cart badge number should be clearly readable.

**Acceptance Scenarios**:
1. **Given** the user is on the Category page, **When** viewing the filters, **Then** they appear in a single row with reduced font size and wrap appropriately on mobile.
2. **Given** there are items in the cart, **When** viewing the cart icon in the app bar, **Then** the badge number is fully visible and not cut off.
3. **Given** the user is on the Category page, **When** viewing the top of the page, **Then** the excessive top margin/padding is removed.

---

### User Story 3 - Polish Product Card Layout (Priority: P2)

Users viewing product cards should see spacing between adjacent cards, sale badges positioned correctly on top of the image, and a default placeholder image if the product image fails to load.

**Why this priority**: Enhances the grid's visual rhythm and ensures a non-broken experience for missing images.

**Independent Test**: View the product grid. Cards should have visual separation. The "Sale!" badge should overlap the image perfectly. Any missing images should display a standard placeholder icon or image instead of a broken browser image icon.

**Acceptance Scenarios**:
1. **Given** a product in the grid, **When** displayed alongside others, **Then** there is adequate spacing between the cards.
2. **Given** a product is on sale, **When** the card renders, **Then** the sale badge is positioned over the image.
3. **Given** a product image URL is invalid, **When** the browser fails to load it, **Then** a placeholder image/icon is shown as a fallback.

## Requirements *(mandatory)*

### Functional Requirements

- **FR-001**: System MUST apply a custom `MudTheme` globally via `MainLayout.razor` with appropriate premium colors (e.g., deep navy, gold/accent, clean backgrounds).
- **FR-002**: System MUST reduce top padding/margin in `Category.razor` to eliminate the reported "too much space".
- **FR-003**: System MUST format the filter toolbar in `Category.razor` to keep items in a single row on desktop, using smaller fonts/components to fit.
- **FR-004**: System MUST ensure the `MudBadge` on the cart icon in `MainLayout.razor` correctly displays its content (adjusting Overlap, placement, or padding).
- **FR-005**: System MUST add margin/gap between product cards in the MudGrid in `Category.razor` to separate images visually.
- **FR-006**: System MUST ensure the Sale badge is positioned on top of the product image using `position-absolute` and z-index.
- **FR-007**: System MUST provide an image fallback mechanism (e.g., `onerror` attribute or a MudBlazor fallback) in `ProductCard.razor` to show a sample image when the primary `ImageUrl` fails.

## Success Criteria *(mandatory)*

### Measurable Outcomes

- **SC-001**: Cart badge number is 100% visible and readable without truncation.
- **SC-002**: Category filters fit on a single horizontal line on screens wider than 768px.
- **SC-003**: 100% of missing or broken images render a cohesive placeholder image instead of the browser's default broken image icon.
- **SC-004**: Theme colors are consistently applied across Appbar, primary buttons, and background surfaces.

## Assumptions

- We will choose a premium fashion color palette (e.g., Dark Slate and Rose Gold, or Monochrome and Accent).
- The MudGrid in `Category.razor` is currently missing spacing (`Spacing="0"` or similar) which needs to be increased.
- MudBlazor's `MudImage` or a standard `<img>` can utilize an `onerror` fallback to a static local image (e.g., `/images/placeholder.png`).
