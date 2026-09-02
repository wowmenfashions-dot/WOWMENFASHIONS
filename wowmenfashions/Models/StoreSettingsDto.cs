namespace wowmenfashions.Models;

public class StoreSettingsDto
{
    public int Id { get; set; } = 1;
    public decimal FreeShippingThreshold { get; set; } = 1000m;
    public decimal ShippingChargeAmount { get; set; } = 100m;
}
