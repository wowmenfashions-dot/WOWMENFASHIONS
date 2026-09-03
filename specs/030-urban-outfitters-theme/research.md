# Research: Urban Outfitters Theme Overhaul

## 1. Font Analysis
- **Decision**: Use `Outfit` (or `Inter`) for headers and secondary text, matching the clean sans-serif look of the reference site.
- **Rationale**: Google Fonts is already imported, we simply need to apply these to the `MudTheme.Typography` configuration globally.

## 2. Color Palette
- **Decision**: Update `MudTheme` palettes.
- **Rationale**: The reference site uses a stark #000000 (black) for buttons, text, and active states, with #FFFFFF (white) backgrounds and #F5F5F5 for subtle off-white backgrounds (cards/footers).
- **Alternatives**: None. MudTheme handles this natively.

## 3. Card Styling
- **Decision**: Override `.mud-card` and product grid CSS.
- **Rationale**: The reference site uses flat cards with no shadows, edge-to-edge images, and left-aligned text directly underneath. MudBlazor default cards use drop shadows and padding. CSS overrides will be placed in `wowmenfashions.styles.css` to strip borders and shadows.
