# Feature Specification: Compact UI Redesign

**Feature Branch**: `008-compact-ui-design`

**Created**: 2026-08-30

**Status**: Draft

**Input**: User description: "you need to think like an experienced UI UX designer. i want this web page to be UI designed very well compact, no spaces. keep the less empty space. user should able to see everything in one single page if possible."
**Clarification**: This applies to the entire application layout (MainLayout.razor and all pages).

## User Scenarios & Testing *(mandatory)*

### User Story 1 - Compact Global Layout (Priority: P1)

As a user, I want the entire application to use a highly dense, compact UI layout with minimized whitespace, padding, and margins so that I can view as much information as possible on a single screen without scrolling.

**Why this priority**: Core user requirement for the UI/UX overhaul.

**Independent Test**: Navigate through the application (Home, Category, Product Details, Cart). Verify that all padding, margins, and component sizing (e.g., inputs, buttons, cards) are in their densest configuration.

**Acceptance Scenarios**:

1. **Given** a user is viewing the application, **When** they look at the top navigation, **Then** the header is slim and compact.
2. **Given** a user is viewing product grids, **When** they browse categories, **Then** the product cards have minimal padding, images are scaled appropriately, and grid gaps are reduced.
3. **Given** a user is viewing a form or filter, **When** they interact with inputs, **Then** all inputs use dense variants.

---

## Requirements *(mandatory)*

### Functional Requirements

- **FR-001**: Update `MainLayout.razor` to reduce container max-widths, remove unnecessary margins/padding, and compact the AppBar/Navigation.
- **FR-002**: Update all `MudBlazor` components (Select, TextField, Button, Table, Grid, Stack) across the app to use `Margin="Margin.Dense"`, `Dense="true"`, and `Size="Size.Small"` where applicable.
- **FR-003**: Reduce padding and margin utility classes (e.g., change `pa-6` to `pa-2`, `mb-12` to `mb-4`) across all pages.
- **FR-004**: Redesign `Category.razor`, `ProductDetails.razor`, and `Cart.razor` to fit more content above the fold (e.g., smaller hero images, tighter text typography).
- **FR-005**: Ensure the compact design is still visually appealing and highly usable ("UI designed very well").

## Success Criteria *(mandatory)*

### Measurable Outcomes

- **SC-001**: A minimum of 30% more content is visible vertically on standard desktop displays without scrolling compared to the previous design.
- **SC-002**: All form inputs and buttons utilize their compact/dense property variants.
- **SC-003**: No large, unbroken empty whitespace blocks remain in the primary content areas.
