# Research Notes: Product Color Variants

## Handling Dynamic Image Switching
- **Decision**: Define a dictionary mapping colors to image URLs within the `ProductDto` (or simulate it in `ProductDetails.razor`) and update the currently displayed image source when a color is clicked.
- **Rationale**: This is the standard way to handle variants in e-commerce applications.
- **Alternatives considered**: Reloading the entire product data from the server (rejected due to latency and poor UX).
