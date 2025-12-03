namespace HotelService.Domain.Entities;

public class Hotel
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string Country { get; set; } = null!;
    public string City { get; set; } = null!;
    public byte Stars { get; set; }
    public string Description { get; set; } = null!;
    
    public IEnumerable<HotelPhoto> Photos { get; set; } = [];
    public IEnumerable<HotelRoom> Rooms { get; set; } = [];
    public IEnumerable<HotelComment> Comments { get; set; } = [];
}