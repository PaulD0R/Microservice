namespace HotelService.Application.Interfaces.Caches;

public interface ICacheService
{
    Task<T?>  GetAsync<T>(string key, CancellationToken ct);
    Task<bool> SetAsync<T>(string key, T value, CancellationToken ct);
}