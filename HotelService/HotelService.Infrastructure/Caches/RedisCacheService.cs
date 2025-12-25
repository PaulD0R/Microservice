using System.Text.Json;
using HotelService.Application.Interfaces.Caches;
using Microsoft.Extensions.Caching.Distributed;

namespace HotelService.Infrastructure.Caches;

public class RedisCacheService(
    IDistributedCache cache)
    : ICacheService
{
    private const int LiveTime = 15;
    public async Task<T?> GetAsync<T>(string key, CancellationToken ct)
    {
        var valueString = await cache.GetStringAsync(key, ct);
        return valueString == null ? default : JsonSerializer.Deserialize<T>(valueString);
    }

    public async Task<bool> SetAsync<T>(string key, T value, CancellationToken ct)
    {
        var options = new DistributedCacheEntryOptions()
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(LiveTime)
        };
        
        var valueString = JsonSerializer.Serialize(value);
        await cache.SetStringAsync(key, valueString, options, ct);
        
        return true;
    }

    public async Task<bool> RemoveAsync(string key, CancellationToken ct)
    {
        await cache.RemoveAsync(key, ct);
        return true;
    }
}