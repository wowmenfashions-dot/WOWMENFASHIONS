using System;

namespace wowmenfashions.Data.Entities;

public class CartItem
{
    public int Id { get; set; }
    public Guid CartId { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public string? SelectedColor { get; set; }
    
    // Additional properties that might be joined from Product
    public string? ProductName { get; set; }
    public decimal UnitPrice { get; set; }
    public string? ProductImageUrl { get; set; }
    
    public decimal TotalPrice => UnitPrice * Quantity;
}
