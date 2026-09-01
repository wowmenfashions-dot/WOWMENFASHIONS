# Feature Specification: State, Caching, and Cart

**Feature Branch**: `006-state-caching-cart`

**Created**: 2026-08-30

**Status**: Draft

**Input**: User description: "new requirement. when the user switch from one navigation menu to another menu it should clear the filter all the details related to the website should be fetched only from sql database. filter dropdowns , products etc. it should be stored in-memory for faster display. the cart page is not visible. use speckit for this lifecucle always."

## User Scenarios & Testing *(mandatory)*

### User Story 1 - Clear Filters on Navigation (Priority: P1)

As a user, when I navigate from one main category (e.g., Men's) to another (e.g., Women's) using the navigation menu, I want my previous filters (price, color, subcategory) to be completely reset so I see all products in the new category.

**Why this priority**: It fixes a frustrating user experience where stale filters hide products when switching contexts.

**Independent Test**: Can be tested by setting a filter (e.g., Max Price 500) in Men's, then clicking "Women's" in the navigation menu, and verifying the Price filter is back to its default state.

**Acceptance Scenarios**:

1. **Given** I am on the Men's category with the Color filter set to "Blue", **When** I click Women's in the side navigation, **Then** the page loads with no color filter applied.

---

### User Story 2 - In-Memory Caching of Database Data (Priority: P1)

As a system, I want to fetch all reference data (categories, products, colors) exclusively from the SQL database but cache it in-memory so that the UI displays instantly without hammering the database on every page load.

**Why this priority**: Requested by user for performance optimization and to guarantee data source purity (SQL only).

**Independent Test**: Can be verified by observing database query logs (or application logs) to ensure the database is queried only once (or periodically) for categories/products, and subsequent page reloads are instantaneous.

**Acceptance Scenarios**:

1. **Given** the application is running, **When** a user accesses the category page for the first time, **Then** the data is fetched from SQL and stored in memory.
2. **Given** the data is cached, **When** a second user accesses the same page, **Then** the data is served from memory without a SQL query.

---

### User Story 3 - Visible and Functional Cart Page (Priority: P1)

As a user, I want to be able to navigate to the `/cart` page and view the items I have added to my shopping cart, because currently the page returns a 404 error.

**Why this priority**: A missing cart page breaks the fundamental e-commerce flow.

**Independent Test**: Can be tested by clicking the shopping cart icon or navigating to `/cart` and verifying a cart interface appears instead of a 404.

**Acceptance Scenarios**:

1. **Given** I have added an item to my cart, **When** I navigate to `/cart`, **Then** I see the item listed with its price and quantity.
2. **Given** I am on any page, **When** I click the cart icon in the app bar, **Then** I am successfully navigated to the Cart page.

---

### Edge Cases

- What happens if the in-memory cache expires or the database is updated? (Cache should be invalidated or have a reasonable sliding/absolute expiration).
- What happens if the cart is empty when navigating to `/cart`? (It should show a friendly "Cart is empty" message).

## Requirements *(mandatory)*

### Functional Requirements

- **FR-001**: The system MUST reset all filter state variables (Price, Color, Subcategory, Sort) to defaults whenever the primary category Slug changes via navigation.
- **FR-002**: The system MUST fetch all product, category, and reference data exclusively from the SQL Database (no hardcoded lists or JSON files).
- **FR-003**: The system MUST utilize an in-memory caching mechanism (e.g., `IMemoryCache`) to store and retrieve data fetched from SQL to improve display speed.
- **FR-004**: The system MUST implement a routable `/cart` Blazor page that displays the current user's cart contents.

### Key Entities

- **Cart/CartItem**: Represents the user's selected products.
- **Product/Category Cache**: The in-memory representation of the SQL data.

## Success Criteria *(mandatory)*

### Measurable Outcomes

- **SC-001**: Filter states are successfully cleared 100% of the time upon primary category navigation.
- **SC-002**: Database queries for read-heavy operations (categories/products) are reduced significantly due to caching.
- **SC-003**: The `/cart` URL returns a valid page (HTTP 200 equivalent) instead of 404.

## Assumptions

- We will use standard .NET `IMemoryCache` for the in-memory storage requirement.
- A standard cache expiration time (e.g., 5-15 minutes) is acceptable for this MVP.
