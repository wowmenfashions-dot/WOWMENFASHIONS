using System;

namespace wowmenfashions.Models;

public class ProductDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty; // Deprecated, but keeping for compatibility
    public int CategoryId { get; set; }
    public string Description { get; set; } = string.Empty;
    public string Color { get; set; } = string.Empty; // Single color fallback
    public string? AvailableColors { get; set; }
    public List<string> AvailableColorsList 
    { 
        get => string.IsNullOrEmpty(AvailableColors) ? new List<string>() : AvailableColors.Split(',').Select(c => c.Trim()).ToList();
    }
    public decimal Price { get; set; }
    public decimal? OriginalPrice { get; set; }
    public string ImageUrl { get; set; } = string.Empty;
    public Dictionary<string, string> ColorImages { get; set; } = new();
    public string ColorImagesString 
    {
        get => string.Join(";\n", ColorImages.Select(kv => $"{kv.Key}={kv.Value}"));
        set 
        {
            ColorImages.Clear();
            if (!string.IsNullOrEmpty(value))
            {
                foreach(var pair in value.Split(';', StringSplitOptions.RemoveEmptyEntries))
                {
                    var parts = pair.Split('=');
                    if(parts.Length == 2) ColorImages[parts[0].Trim()] = parts[1].Trim();
                }
            }
        }
    }
    public List<string> AdditionalImages { get; set; } = new();

    public bool IsOnSale => OriginalPrice.HasValue && OriginalPrice.Value > Price;
}
