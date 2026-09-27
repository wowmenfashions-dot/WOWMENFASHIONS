using System;

namespace wowmenfashions.Data.Entities;

public class Review
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public int CustomerId { get; set; }
    public int Rating { get; set; }
    public string? Comment { get; set; }
    public string? AdminReply { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    
    // Navigation properties
    public wowmenfashions.Models.ProductDto Product { get; set; } = null!;
    public Customer Customer { get; set; } = null!;
}
