namespace HotelService.Application.Interfaces.Repositories;

public interface ICloudRepository
{
    Task<string> GetUploadUrlAsync(string key, string contentType, CancellationToken ct);
    Task<string> GetDownloadUrlAsync(string key, CancellationToken ct);
    Task<bool> DeleteAsync(string key, CancellationToken ct);
}