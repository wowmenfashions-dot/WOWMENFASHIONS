using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using wowmenfashions.Data;

namespace wowmenfashions.Services;

public interface IHomepageConfigService
{
    Task<string> GetAnnouncementAsync();
    Task SetAnnouncementAsync(string announcement);
    
    Task<List<string>> GetCarouselImagesAsync();
    Task SetCarouselImagesAsync(List<string> images);
}

public class HomepageConfigService : IHomepageConfigService
{
    private readonly IHomepageConfigRepository _repository;

    public HomepageConfigService(IHomepageConfigRepository repository)
    {
        _repository = repository;
    }

    public async Task<string> GetAnnouncementAsync()
    {
        var config = await _repository.GetConfigAsync("Announcement");
        return config != null ? config.ConfigValue : "Welcome to WOWMEN FASHIONS! Enjoy 20% off your first order.";
    }

    public async Task SetAnnouncementAsync(string announcement)
    {
        await _repository.SetConfigAsync("Announcement", announcement);
    }

    public async Task<List<string>> GetCarouselImagesAsync()
    {
        var config = await _repository.GetConfigAsync("Carousel");
        if (config != null && !string.IsNullOrWhiteSpace(config.ConfigValue))
        {
            try
            {
                return JsonSerializer.Deserialize<List<string>>(config.ConfigValue) ?? new List<string>();
            }
            catch
            {
                // Fallback if invalid JSON
            }
        }
        
        // Default images if not configured
        return new List<string>
        {
            "/images/hero.webp",
            "/images/hero-1.webp",
            "/images/hero-2.webp"
        };
    }

    public async Task SetCarouselImagesAsync(List<string> images)
    {
        var json = JsonSerializer.Serialize(images);
        await _repository.SetConfigAsync("Carousel", json);
    }
}
