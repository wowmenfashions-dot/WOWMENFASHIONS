namespace wowmenfashions.Services;

using System.Threading.Tasks;
using wowmenfashions.Models;

public interface IStoreSettingsService
{
    Task<StoreSettingsDto> GetStoreSettingsAsync();
    Task<bool> UpdateStoreSettingsAsync(StoreSettingsDto settings);
}
