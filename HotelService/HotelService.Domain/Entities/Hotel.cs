namespace HotelService.Domain.Entities;

public class Hotel
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string Country { get; set; } = null!;
    public string City { get; set; } = null!;
    public string FullAddress { get; set; } = null!;
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public byte Stars { get; set; }
    public string Description { get; set; } = null!;
    public decimal RealRating { get; set; } = 0;
    public decimal ShownRating { get; set; } = 0;
    public decimal MinPrice { get; set; } = decimal.MaxValue;
    
    public IEnumerable<HotelPhoto> Photos { get; set; } = [];
    public IEnumerable<HotelRoom> Rooms { get; set; } = [];
    public IEnumerable<HotelComment> Comments { get; set; } = [];
}