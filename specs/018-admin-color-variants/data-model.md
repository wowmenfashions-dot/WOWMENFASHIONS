# Data Model: Admin Product Color Variants

## Entities

### `ProductColorImage` (New Entity)

Represents the mapping between a specific product, a specific color, and the corresponding image URL.

```csharp
public class ProductColorImage
{
    public int Id { get; set; }
    
    [Required]
    public int ProductId { get; set; }
    public Product Product { get; set; } = null!;
    
    [Required]
    [MaxLength(50)]
    public string ColorName { get; set; } = string.Empty;
    
    [Required]
    [MaxLength(255)]
    public string ImageUrl { get; set; } = string.Empty;
}
```

### `Product` (Updated Entity)

```csharp
public class Product
{
    // ... existing fields ...
    
    public ICollection<ProductColorImage> ColorImages { get; set; } = new List<ProductColorImage>();
}
```

## EF Core Configuration

We need to add a `DbSet<ProductColorImage>` to `AppDbContext` and configure the foreign key relationship in `OnModelCreating`.
