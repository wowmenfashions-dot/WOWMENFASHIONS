namespace wowmenfashions.Repositories;

using System.Threading.Tasks;
using wowmenfashions.Models;

public interface IStoreSettingsRepository
{
    Task<StoreSettingsDto> GetStoreSettingsAsync();
    Task<bool> UpdateStoreSettingsAsync(StoreSettingsDto settings);
}
