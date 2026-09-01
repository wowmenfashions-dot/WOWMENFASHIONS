using System;
using System.Collections.Generic;

namespace wowmenfashions.Data.Entities;

public class Cart
{
    public Guid Id { get; set; }
    public int? CustomerId { get; set; }
    public Guid? GuestCartId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    
    // Navigation property, populated by Dapper multi-mapping if needed
    public List<CartItem> Items { get; set; } = new();
}
