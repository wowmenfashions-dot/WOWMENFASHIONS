using System.Threading.Tasks;
using wowmenfashions.Models;
using wowmenfashions.Repositories;

namespace wowmenfashions.Services;

public class StoreSettingsService : IStoreSettingsService
{
    private readonly IStoreSettingsRepository _repository;

    public StoreSettingsService(IStoreSettingsRepository repository)
    {
        _repository = repository;
    }

    public async Task<StoreSettingsDto> GetStoreSettingsAsync()
    {
        return await _repository.GetStoreSettingsAsync();
    }

    public async Task<bool> UpdateStoreSettingsAsync(StoreSettingsDto settings)
    {
        if (settings.FreeShippingThreshold < 0 || settings.ShippingChargeAmount < 0)
        {
            throw new ArgumentException("Threshold and charge amount must be non-negative.");
        }
        return await _repository.UpdateStoreSettingsAsync(settings);
    }
}
