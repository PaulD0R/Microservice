namespace HotelService.Domain.Entities;

public class RoomPhoto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string Key { get; set; } = null!;
    public string Type { get; set; } = null!;             
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}