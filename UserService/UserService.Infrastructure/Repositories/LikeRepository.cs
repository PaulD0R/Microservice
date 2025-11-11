using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;
using UserService.Application.Interfaces.Repositories;
using UserService.Domain.Entities;

namespace UserService.Infrastructure.Repositories;

public class LikeRepository(IDistributedCache cache) : ILikeRepository
{

    public async Task<Like?> GetByUserIdAsync(string userId)
    {
        var cacheKey = GetCacheKey(userId);
        var cachedData = await cache.GetStringAsync(cacheKey);

        return string.IsNullOrEmpty(cachedData) ? null : JsonSerializer.Deserialize<Like>(cachedData);
    }

    public async Task SaveAsync(Like userLikes)
    {
        var cacheKey = GetCacheKey(userLikes.PersonId);
        var serializedData = JsonSerializer.Serialize(userLikes);

        var options = new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(30)
        };

        await cache.SetStringAsync(cacheKey, serializedData, options);
    }

    private static string GetCacheKey(string userId) => $"user_likes:{userId}";
}