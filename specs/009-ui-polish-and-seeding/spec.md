# Feature Specification: UI Polish and Database Seeding Expansion

**Feature Branch**: `009-ui-polish-and-seeding`

**Created**: 2026-08-30

**Status**: Draft

**Input**: User description: 
1. UI Fixes: Fix product card heights, image fallbacks, typography, and button placements (from prior feedback).
2. Seeding: Add 100 dummy products for Men, 100 for Women. Add new categories for Kids, Toddler, Adults, Teenager, and add 50 products for each. Add random images (AVIF format only, per Constitution Principle 29).

## User Scenarios & Testing *(mandatory)*

### User Story 1 - Uniform UI Card Redesign (Priority: P1)

As a user, I want the product cards in the grid to be completely uniform in height and visually aligned, regardless of whether the product image fails to load or is a different aspect ratio, so that the grid looks professional.

**Why this priority**: Directly addresses user feedback regarding the "very bad" broken UI in the dense grid.

**Independent Test**: Navigate to a category page with broken image links. The cards MUST all be identical in height, with the "Add" button always pushed to the bottom.

**Acceptance Scenarios**:
1. **Given** a product card with a missing image, **When** rendered, **Then** a fallback colored block with fixed height (e.g., 200px) is shown instead of collapsing.
2. **Given** multiple cards in a row, **When** rendered on desktop, **Then** they stretch to equal heights using flexbox.
3. **Given** the "Add to Cart" button, **When** rendered, **Then** it is located at the bottom of the card content area, not floating over the image.

### User Story 2 - Massive Database Seeding & Categories (Priority: P2)

As a developer, I want a robust SQL script to generate 400 dummy products across 6 categories with random `.avif` image links so that I can properly stress-test the UI grid, pagination, and SQL performance.

**Why this priority**: Required to populate the UI with realistic data volumes.

**Independent Test**: Execute the SQL seed script. Verify the `Categories` table contains Men, Women, Kids, Toddler, Adults, and Teenager. Verify the `Products` table contains exactly 400 new products distributed according to the requirements.

**Acceptance Scenarios**:
1. **Given** an empty or existing database, **When** the seeding script runs, **Then** Men and Women categories get 100 products each.
2. **Given** the seeding script, **When** it runs, **Then** Kids, Toddler, Adults, and Teenager categories are created (if they don't exist) and get 50 products each.
3. **Given** the generated products, **When** checked, **Then** all `ImageUrl` fields must end in `.avif`.

---

## Requirements *(mandatory)*

### Functional Requirements

- **FR-001**: Modify `ProductCard.razor` to enforce strict equal heights across grid rows (`d-flex flex-column h-100`).
- **FR-002**: Move the "Add" button to the bottom of the `MudCardContent` area using `mt-auto`.
- **FR-003**: Provide a fallback mechanism or strict `200px` placeholder for `MudImage` when the URL fails to load.
- **FR-004**: Increase the font size for the Product Title to `Typo.subtitle2` or `Typo.body1` for better readability.
- **FR-005**: Write a SQL script (`02_SeedDummyData.sql`) using `WHILE` loops to insert the requested 400 dummy products and the required categories.
- **FR-006**: Ensure all randomly generated image URLs in the SQL script end with `.avif`.

## Success Criteria *(mandatory)*

### Measurable Outcomes

- **SC-001**: All product cards in a grid row are exactly the same height.
- **SC-002**: 400 new products are successfully inserted into the SQL Server database.
- **SC-003**: 100% of the new products have `.avif` image URLs, complying with Constitution Principle 29.
