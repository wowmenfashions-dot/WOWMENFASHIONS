using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using wowmenfashions.Data.Entities;

namespace wowmenfashions.Services;

public interface IHomepageImageService
{
    Task<List<HomepageImage>> GetCachedImagesAsync();
    Task SaveImagesAsync(List<HomepageImage> images);
    Task<byte[]> ProcessImageAsync(Stream fileStream);
}
