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
    public string? AvailableColorsString { get; set; }
    public List<string> AvailableColors 
    { 
        get => string.IsNullOrEmpty(AvailableColorsString) ? new List<string>() : AvailableColorsString.Split(',').Select(c => c.Trim()).ToList();
        set => AvailableColorsString = value != null ? string.Join(",", value) : null;
    }
    public decimal Price { get; set; }
    public decimal? OriginalPrice { get; set; }
    public string ImageUrl { get; set; } = string.Empty;
    public List<string> AdditionalImages { get; set; } = new();

    public bool IsOnSale => OriginalPrice.HasValue && OriginalPrice.Value > Price;
}
