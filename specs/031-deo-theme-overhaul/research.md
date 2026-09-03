# Research: Deo Theme Overhaul

## 1. Needs Clarification & Findings
- **How to apply pill shapes globally in MudBlazor?**
  - **Decision:** Set `DefaultBorderRadius` to `9999px` in `MainLayout.razor`'s `LayoutProperties`. MudBlazor automatically uses this variable for most components (buttons, text fields). We will additionally enforce `.mud-button-filled` and `.mud-input` using CSS in `app.css` to be absolutely certain.
- **How to handle Product Card hover states without shadows?**
  - **Decision:** Deo eCommerce template usually uses subtle opacity shifts on images rather than lifting the whole card. We will implement a `.gallery-image:hover` scale or an opacity fade instead of the `hover-lift` box-shadow.

## 2. Best Practices & Rationale
- **Rationale for Soft Charcoal (`#1E1E1E`):** Pure black (`#000000`) creates extremely harsh contrast against pure white backgrounds on modern HDR displays, which contradicts the "soft, luxurious" vibe of the Deo template. Soft charcoal mitigates this.
- **Rationale for Light Gray Product Backdrops (`#F3F4F6`):** Provides a visual bounding box for products without needing harsh 1px borders or drop shadows. Using `rounded-2xl` on the background specifically softens the rigid grid layout.
