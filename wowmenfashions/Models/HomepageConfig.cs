using System;

namespace wowmenfashions.Models;

public class HomepageConfig
{
    public int Id { get; set; }
    public string ConfigKey { get; set; } = string.Empty;
    public string ConfigValue { get; set; } = string.Empty;
    public DateTime UpdatedAt { get; set; }
}
