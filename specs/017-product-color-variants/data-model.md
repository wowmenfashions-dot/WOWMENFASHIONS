# Data Model: Product Color Variants

We will add a new property to `ProductDto`:
```csharp
public Dictionary<string, string> ColorImages { get; set; } = new();
```
This will allow mapping a color name (e.g. "Black") to a specific image URL.
