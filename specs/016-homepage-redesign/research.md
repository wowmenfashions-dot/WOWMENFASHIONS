# Research Notes: Homepage Redesign

## UI Component Selection
- **Decision**: Use `MudCarousel` for the dummy images.
- **Rationale**: `MudCarousel` is the built-in MudBlazor component for sliding images and perfectly fits the requirement without pulling in external JavaScript libraries.
- **Alternatives considered**: Writing a custom CSS/JS carousel (rejected due to added complexity and wheel-reinvention).

## Layout Strategy
- **Decision**: Wrap the main content in a container with `height: calc(100vh - [header_height])` and `overflow: hidden` on desktop, but `overflow-y: auto` on mobile.
- **Rationale**: Meets the user's specific requirement to prevent vertical scrolling on desktop while adhering to the clarification that mobile can scroll if needed.
- **Alternatives considered**: Absolute positioning (rejected due to poor responsive scaling).
