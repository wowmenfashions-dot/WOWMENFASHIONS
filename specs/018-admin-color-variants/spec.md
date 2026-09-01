# Feature Specification: Admin Product Color Variants Management

**Feature Branch**: `018-admin-color-variants`

**Created**: 2026-08-31

**Status**: Draft

**Input**: User description: "when i change the color i am not seeing the corresponding colored product. why add some sample image. how admin can add a new product imge etc."

## User Scenarios & Testing *(mandatory)*

### User Story 1 - Admin Image Upload for Colors (Priority: P1)

The admin should be able to upload or associate specific images with the available colors of a product from the Admin dashboard.

**Why this priority**: It solves the core problem of assigning correct variant images so the user frontend can dynamically switch them.

**Independent Test**: Can be tested by logging into the Admin dashboard, editing a product, uploading a new image and associating it with a specific color, saving it, and verifying it works on the frontend.

**Acceptance Scenarios**:

1. **Given** an admin is editing a product, **When** they define available colors, **Then** they have an option to upload/link an image specifically for each color.
2. **Given** a user is on the product page, **When** they select a color swatch, **Then** the main image dynamically switches to the image the admin assigned to that color.

## Requirements *(mandatory)*

### Functional Requirements

- **FR-001**: System MUST allow admins to assign images to specific color variants of a product.
- **FR-002**: Database schema MUST be updated to support a mapping between Product ID, Color Name, and Image URL.
- **FR-003**: The `ProductService` MUST retrieve these color-image mappings and populate the `ProductDto.ColorImages` property dynamically from the database instead of hardcoded values.
- **FR-004**: The `ProductDetails.razor` frontend MUST properly use these dynamic mappings to swap the image on click.

## Success Criteria *(mandatory)*

### Measurable Outcomes

- **SC-001**: Admin dashboard successfully saves color-image mappings to the database.
- **SC-002**: Frontend dynamically loads and switches images based on the database mappings with 0 hardcoded values.

## Assumptions

- We will create a new table `ProductColorImages` or similar to handle the mapping.
- The Admin dashboard (`Dashboard.razor` or `AdminProducts.razor`) will be updated with a new UI section during product edit.
