namespace HotelService.Domain.Entities;

public class HotelPhoto
{
    public Guid Id { get; set; }
    public Guid HotelId { get; set; }
    public string Name { get; set; } = null!;
    public string Key { get; set; } = null!;
    public string Type { get; set; } = null!;            
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}