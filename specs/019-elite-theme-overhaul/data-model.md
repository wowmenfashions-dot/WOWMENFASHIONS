# Data Model: Elite UI Theme Overhaul

*(No database or backend data model changes required for this frontend styling feature)*

## Theme Configuration Model (Frontend State)

```csharp
MudTheme EliteTheme = new MudTheme()
{
    PaletteLight = new PaletteLight()
    {
        Primary = "#1A1A1A", // Charcoal
        Background = "#FAFAFA", // Off-white
        Surface = "#FFFFFF",
        AppbarBackground = "#1A1A1A",
        TextPrimary = "#1A1A1A",
        TextSecondary = "#757575",
        Success = "#8B5A2B" // Cognac Leather / Accent
    },
    Typography = new Typography()
    {
        Default = new Default() { FontFamily = new[] { "Inter", "sans-serif" } },
        H1 = new H1() { FontFamily = new[] { "Playfair Display", "serif" } },
        H2 = new H2() { FontFamily = new[] { "Playfair Display", "serif" } },
        H3 = new H3() { FontFamily = new[] { "Playfair Display", "serif" } },
        H4 = new H4() { FontFamily = new[] { "Playfair Display", "serif" } },
        H5 = new H5() { FontFamily = new[] { "Playfair Display", "serif" } },
        H6 = new H6() { FontFamily = new[] { "Playfair Display", "serif" } },
    }
};
```
