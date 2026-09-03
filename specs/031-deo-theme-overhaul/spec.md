# Deo eCommerce Theme Overhaul (UI/UX)

## 1. Description
This feature overhauls the global theme and aesthetic of the WOWMENFASHIONS Blazor application to match the modern, luxury, minimalist aesthetic of the "Deo eCommerce" Next.js template. This replaces the previous Urban Outfitters blocky/minimalist theme with a softer, pill-geometry-focused design language.

## 2. Goals
- Transition the UI from a sharp-edged, high-contrast blocky style to a soft, rounded geometry (`pill-shaped` buttons and inputs, `rounded-2xl` images).
- Implement a color palette focusing on crisp white backgrounds, light neutral gray (`#F3F4F6`) product image backdrops, and soft charcoal black (`rgba(0,0,0,0.8)`) for primary text and calls-to-action.
- Refine typography to use a clean geometric sans-serif (Inter/Geist aesthetic) with tight leading and medium-to-bold headings.
- Overhaul Product Cards: Provide generous rounded corners on the image container with a light gray background, removing all shadows and borders, while vertically stacking the brand/category, title, and price metadata below.

## 3. Scope
- Global CSS updates (MudBlazor theme overrides in `MainLayout.razor` and global CSS in `app.css`).
- Redesign of the `ProductCard.razor` component (image backdrops, text alignment, corner radius).
- Re-styling of all primary/secondary buttons and input fields to adopt the full pill-shape (`rounded-full` / `border-radius: 9999px`) styling.
- Verification of the new aesthetic across the Homepage, Catalog, and Product Detail sections.

## 4. Requirements & Non-Goals

### 4.1 Functional Requirements
- MUST override all global instances of `.mud-card`, `.mud-button-filled`, `.mud-button-outlined`, and `.mud-input` to enforce the new pill and rounded geometry.
- MUST apply soft charcoal black (`#1E1E1E` or `rgba(0,0,0,0.8)`) as the primary text and background color for active elements.
- MUST use a light gray (`#F3F4F6` or `bg-gray-100`) backdrop for product images to create a distinct, modern contrast against the pure white body background.

### 4.2 Non-Functional Requirements
- MUST maintain existing responsive behaviors (mobile grids, touch targets).
- MUST remain performant without heavy CSS animations or massive image filters.

### 4.3 Non-Goals
- NOT migrating the architecture to Next.js or React. We are strictly matching the visual aesthetic within the existing Blazor framework.
- NOT altering the database schema, business logic, checkout flows, or backend integrations.

## 5. User Scenarios
**Scenario 1: Browsing the Catalog**
When a user visits the catalog, they see a grid of products where each image sits cleanly on a soft gray, heavily-rounded backdrop (`rounded-2xl`). The metadata below is left-aligned with a muted subtitle and a soft-black product title, giving a luxurious, modern feel.

**Scenario 2: Interacting with Forms/Buttons**
When a user adds an item to their cart or filters products, they interact with completely pill-shaped buttons (`rounded-full`). The primary buttons are solid soft black with crisp white text, avoiding harsh jet black.

## 6. Definitions
- **Pill Shape / Geometry**: A shape where the border radius is fully rounded (e.g., `border-radius: 9999px` or `rounded-full`).
- **Soft Charcoal Black**: A dark color (`#1E1E1E` or `rgba(0,0,0,0.8)`) used instead of pure black (`#000000`) to reduce eye strain and provide a premium feel.
