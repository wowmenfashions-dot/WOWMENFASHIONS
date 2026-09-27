# Research

## UI Changes and MudBlazor

- **Decision**: Use `MudNavMenu` for navigation, setting its default state to collapsed or hidden. We will customize the hamburger menu toggle icon (typically a `MudIconButton` with `Icons.Material.Filled.Menu`) by applying a `Color="Color.Error"` or custom red styling to ensure it meets the design requirements.
- **Rationale**: The project constitution mandates MudBlazor. Using standard MudBlazor components with parameter customization is the most efficient and maintainable approach.
- **Alternatives considered**: Writing custom CSS for a standard HTML button, which would violate the consistency of the MudBlazor-based UI.

## Color Variants UI

- **Decision**: Use a custom `MudChip` group or styled div elements to represent color variants. When selected, the element will receive a distinctive border/outline class as specified in the reference image.
- **Rationale**: Provides clear visual feedback without requiring complex custom components.
- **Alternatives considered**: Using `MudSelect` (dropdown), but this contradicts the visual requirement shown in the reference image.

## Product Image Zoom

- **Decision**: Implement a MudBlazor `MudDialog` or `MudOverlay` triggered by clicking a zoom icon on the product image.
- **Rationale**: MudBlazor provides these components out-of-the-box, ensuring a consistent user experience and avoiding external dependencies.
- **Alternatives considered**: Importing a third-party JS library (e.g., Lightbox2), but this adds unnecessary overhead and potential conflicts with Blazor Server's rendering model.

## Category Image Format

- **Decision**: All new promotional images for the Men's category must be served in AVIF format.
- **Rationale**: Principle 29 of the project constitution explicitly mandates AVIF for all application images.
- **Alternatives considered**: None, as this is a strict constitutional requirement.
