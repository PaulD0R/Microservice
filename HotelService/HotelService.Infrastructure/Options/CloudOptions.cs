namespace HotelService.Infrastructure.Options;

public class CloudOptions
{
    public string BucketName { get; set; } = null!;
    public string ServiceUrl { get; init; } = null!;
}