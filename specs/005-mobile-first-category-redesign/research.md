# Research: Mobile-First Category Redesign

## Decision: Horizontal Filter Layout
**Decision**: Use `MudStack` or `MudGrid` with responsive breakpoints (`xs`, `sm`, `md`) to stack the MudSelect components on top of the product grid.
**Rationale**: Adheres to Principle 29 (Mobile-First). Stacking vertically on mobile and horizontally on desktop maximizes space for the product grid on smaller screens.
**Alternatives considered**: Keep the sidebar and hide it behind a hamburger menu on mobile (adds more clicks for users, less discoverable).

## Decision: Product Card Clickability
**Decision**: Wrap the `MudCard` or place an `@onclick` handler on the entire card to navigate to `/product/{Id}`.
**Rationale**: E-commerce standard is that the entire product card surface is clickable.
**Alternatives considered**: Only making the image and title clickable (creates a frustrating user experience with "dead" zones on the card).

## Decision: Product Details Layout (MudBlazor Content 3 Wireframe)
**Decision**: Redesign `ProductDetails.razor` to mimic MudBlazor's Content 3 wireframe (large hero image/carousel on one side, detailed stacked info on the right on desktop; stacked fully vertically on mobile).
**Rationale**: Explicitly requested by the user.
**Alternatives considered**: N/A, user provided a direct reference.
