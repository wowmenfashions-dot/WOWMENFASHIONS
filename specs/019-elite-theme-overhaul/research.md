# Research Notes: Elite UI Theme Overhaul

## Handling Font Imports in Blazor 8/9 Web Apps
- **Decision**: Add `<link>` tags for Google Fonts (`Playfair Display` and `Inter`) directly in `App.razor` `<head>` section.
- **Rationale**: This is the standard entry point for global HTML metadata and styles in modern Blazor interactive apps.
- **Alternatives considered**: CSS `@import` (rejected due to slower render-blocking performance).

## Overriding MudBlazor Themes
- **Decision**: Create a custom `MudTheme` instance in `MainLayout.razor` and pass it to `<MudThemeProvider Theme="@MyCustomTheme" />`.
- **Rationale**: MudBlazor natively supports extensive theme customization via C# objects, allowing us to change the primary color, background color, and font families globally without writing fragile CSS overrides.
- **Alternatives considered**: Using `!important` CSS rules everywhere (rejected as bad practice).
