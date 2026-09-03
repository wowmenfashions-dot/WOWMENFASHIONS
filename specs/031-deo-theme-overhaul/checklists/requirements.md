# Deo eCommerce Theme Overhaul - Requirements Checklist

- [ ] Global Theme Configuration
  - [ ] Update `MainLayout.razor` to use Soft Charcoal Black (`#1E1E1E`) as the primary theme color.
  - [ ] Set `DefaultBorderRadius` to `9999px` to enforce pill-shapes on generic MudBlazor components by default.
  - [ ] Keep geometric sans-serif fonts (Inter/Outfit) but verify font weights are appropriately soft (Medium/Semi-bold instead of heavy Bold).

- [ ] Global CSS Overrides (`app.css`)
  - [ ] Override `.mud-button-filled` to enforce fully rounded pill shapes, soft black background, and white text.
  - [ ] Override `.mud-button-outlined` to have a pill shape and a soft gray border.
  - [ ] Override `.mud-input` and `.mud-input-control` to enforce pill-shaped inputs (`border-radius: 9999px`).

- [ ] Product Cards (`ProductCard.razor`)
  - [ ] Apply `rounded-2xl` equivalent border radius (e.g., `16px`) to the product image container.
  - [ ] Apply the soft gray background (`#F3F4F6`) to the image container.
  - [ ] Ensure the overall `.mud-card` wrapper has no borders, no backgrounds, and no shadows.
  - [ ] Left-align all typography (Brand/Category, Title, Price) directly under the image.
  - [ ] Style the text: Category should be muted (`#6B7280`), Title should be soft-black and medium weight, Price should be medium weight.

- [ ] Cross-Cutting Validation
  - [ ] Verify the aesthetic on the Homepage.
  - [ ] Verify the aesthetic on the Catalog Grid (`Category.razor`).
  - [ ] Ensure all hover states (buttons, cards) match the subtle aesthetic (opacity fades or slight gray shifts, no heavy drop-shadows).
