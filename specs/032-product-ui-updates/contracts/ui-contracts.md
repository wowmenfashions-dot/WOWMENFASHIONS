# Contracts

## UI Component Contracts

The following describes the expected behaviors of the updated UI components.

### Navigation Menu (`NavMenu.razor` or similar)
- **Input**: None.
- **Output**: Collapsed by default. Toggled via a hamburger icon.
- **Styling**: Hamburger icon must have `Color="Color.Error"` or equivalent red styling.

### Product Image Zoom (`ProductImageGallery.razor`)
- **Input**: List of `ProductImage` objects.
- **Behavior**: Clicking the zoom icon triggers an overlay/modal displaying the full-resolution AVIF image.

### Color Selector (`ColorSelector.razor`)
- **Input**: List of `ProductColor` objects, Action callback for selection change.
- **Behavior**: Clicking a color swatch applies an active CSS class (e.g., a prominent border) to indicate selection.
