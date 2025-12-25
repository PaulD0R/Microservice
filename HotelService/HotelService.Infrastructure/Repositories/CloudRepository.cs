using Amazon.S3;
using Amazon.S3.Model;
using HotelService.Application.Interfaces.Repositories;
using HotelService.Infrastructure.Options;
using Microsoft.Extensions.Options;

namespace HotelService.Infrastructure.Repositories;

public class CloudRepository(
    IAmazonS3 cloud,
    IOptions<CloudOptions> options) : ICloudRepository
{
    private const int LiveTime = 10;
    
    public Task<string> GetUploadUrlAsync(string key, string contentType, CancellationToken ct)
    {
        var request = new GetPreSignedUrlRequest
        {
            BucketName = options.Value.BucketName,
            Key = key,
            Verb = HttpVerb.PUT,
            ContentType = contentType,
            Expires = DateTime.UtcNow.AddMinutes(LiveTime)
        };

        return Task.FromResult(cloud.GetPreSignedURL(request));
    }

    public Task<string> GetDownloadUrlAsync(string key, CancellationToken ct)
    {
        var request = new GetPreSignedUrlRequest
        {
            BucketName = options.Value.BucketName,
            Key = key,
            Verb = HttpVerb.GET,
            Expires = DateTime.UtcNow.AddMinutes(LiveTime)
        };

        return Task.FromResult(cloud.GetPreSignedURL(request));
    }

    public async Task<bool> DeleteAsync(string key, CancellationToken ct)
    {
        await cloud.DeleteObjectAsync(options.Value.BucketName, key, ct);
        return true;
    }
}