# Feature Specification: Admin Panel, Multiple Images, & Pagination

**Feature Branch**: `011-admin-and-product-images`

**Created**: 2026-08-30

**Status**: Draft

**Input**: User description: "Change application title to WOWMEN. Add multiple images to a product and display them on product page. Add an admin page strictly for admin users to add/update products and upload images. Add pagination to the category pages."

## User Scenarios & Testing *(mandatory)*

### User Story 1 - Secure Admin Login & Dashboard (Priority: P1)

Admins need a secure way to log into the application so that they can access the product management dashboard without unauthorized users getting in.

**Why this priority**: Essential for securing the administrative capabilities.

**Independent Test**: Navigate to `/admin`. The system should redirect to a login page. After entering valid admin credentials, the user should be redirected to the admin dashboard.

**Acceptance Scenarios**:
1. **Given** an unauthenticated user, **When** they navigate to `/admin`, **Then** they are redirected to `/login`.
2. **Given** a user is on the `/login` page, **When** they provide valid admin credentials, **Then** they are logged in and redirected to the dashboard.

---

### User Story 2 - Admin Product Management (Priority: P1)

Admins need a dashboard to view, add, edit, and delete products, including managing multiple images for a single product, so that they can maintain the catalog.

**Why this priority**: Core business capability to manage inventory.

**Independent Test**: Log in as an admin and navigate to the Products dashboard. Add a new product with multiple images, verify it appears in the list. Edit the product, change its details, and verify the changes persist.

**Acceptance Scenarios**:
1. **Given** an admin is on the Product Management page, **When** they click "Add Product", **Then** a form opens allowing them to input details and upload multiple image URLs.
2. **Given** an existing product, **When** the admin edits it, **Then** they can add or remove images from the product's gallery.

---

### User Story 3 - Multiple Product Images Display (Priority: P2)

Customers viewing a product details page should see all images associated with that product (e.g., as a carousel or thumbnail gallery) to better evaluate the item.

**Why this priority**: Enhances the shopping experience and increases conversion.

**Independent Test**: Navigate to a product page that has multiple images. The primary image should be displayed large, with smaller thumbnails or a carousel allowing the user to view the other images.

**Acceptance Scenarios**:
1. **Given** a product with multiple images, **When** viewing the product details page, **Then** all images are accessible via thumbnails or a carousel.
2. **Given** the user clicks a thumbnail, **When** the action completes, **Then** the primary large image updates to show the selected image.

---

### User Story 4 - Category Page Pagination (Priority: P2)

Customers browsing a category with many products should see paginated results (e.g., 20 products per page) to improve loading speed and navigation.

**Why this priority**: Necessary for performance as the product catalog grows (we recently seeded 400 products).

**Independent Test**: Navigate to the Men's category. Only the first page of results should load. Clicking "Next" or a page number should load the subsequent products.

**Acceptance Scenarios**:
1. **Given** a category with 100 products, **When** viewing the category page, **Then** only the first N products (e.g., 20) are shown, along with pagination controls.
2. **Given** the user clicks page 2, **When** the page updates, **Then** the next batch of products is displayed.

---

### User Story 5 - Rebranding (Priority: P3)

The application title should be updated to "WOWMEN" globally (in the browser tab and the App bar) to reflect the correct brand name.

**Why this priority**: Quick branding fix.

**Independent Test**: View the browser tab title and the top navigation bar.

**Acceptance Scenarios**:
1. **Given** the application is loaded, **When** viewing the title, **Then** it says "WOWMEN".

## Requirements *(mandatory)*

### Functional Requirements

- **FR-001**: System MUST rename the application title to "WOWMEN" in `MainLayout.razor` and `index.html`/`App.razor`.
- **FR-002**: System MUST implement a secure Cookie-based Authentication system for Blazor.
- **FR-003**: System MUST provide a `/login` page that accepts a username and password.
- **FR-004**: System MUST secure the `/admin` routes using the `[Authorize]` attribute.
- **FR-005**: System MUST support multiple images per product by updating the database schema (e.g., a new `ProductImages` table).
- **FR-006**: System MUST provide a UI in the Admin panel to upload or link multiple images per product.
- **FR-007**: System MUST display a product image gallery (carousel or thumbnails) on `ProductDetails.razor`.
- **FR-008**: System MUST implement server-side pagination for products in `Category.razor`.
- **FR-009**: System MUST display a `MudPagination` component on `Category.razor` to navigate between pages.

### Key Entities

- **AdminUser**: Represents the administrator credentials (Username, PasswordHash).
- **ProductImage**: Represents a single image associated with a product (Id, ProductId, ImageUrl).
- **Product**: Will be updated to relate to multiple `ProductImage` entities.

## Success Criteria *(mandatory)*

### Measurable Outcomes

- **SC-001**: The admin panel is completely inaccessible to unauthenticated users (returns 401/redirects to login).
- **SC-002**: A product can successfully have 3+ images associated with it.
- **SC-003**: The category page loads only the specified page size (e.g., 20 items) from the database rather than all 400 at once.
- **SC-004**: The brand name "WOWMEN" is consistently used across the UI.

## Assumptions

- **Admin Login Suggestion**: We will use a simple Cookie Authentication scheme configured in `Program.cs`. For MVP, we will create a single Admin user in the database or hardcode the credentials securely, based on best practices.
- **Pagination**: We will default to 20 products per page on the Category view.
- **Images**: We will continue using URLs (including AVIF formats per Principle 29) for images. The admin can either provide an AVIF URL or we will configure upload logic if they prefer file uploads. For MVP, allowing the admin to paste multiple image URLs is the most robust approach that aligns with the current seeding logic.
