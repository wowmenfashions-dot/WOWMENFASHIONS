# Research: Compact UI Redesign

## Decision: MudBlazor Density Properties
**Decision**: We will rely heavily on MudBlazor's built-in density properties: `Dense="true"`, `Margin="Margin.Dense"`, and `Size="Size.Small"`.
**Rationale**: This prevents us from having to write hundreds of lines of custom CSS overrides. MudBlazor natively supports compacting its components (especially form inputs, tables, and buttons) via these properties.

## Decision: Margin/Padding Utilities
**Decision**: We will replace spacing classes globally. E.g., `pa-6` becomes `pa-2`, `mt-8` becomes `mt-3`, `mb-12` becomes `mb-4`. `Spacing="3"` on `MudGrid` will become `Spacing="1"` or `Spacing="2"`.
**Rationale**: The user explicitly requested "no spaces. keep the less empty space." Drastically cutting the 1-to-12 scale spacing variables achieves this globally.

## Decision: Global Theme
**Decision**: We will update the `MudThemeProvider` in `MainLayout.razor` to reduce default typography margins and overall layout padding if possible.
