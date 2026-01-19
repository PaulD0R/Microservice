namespace HotelService.Domain.Entities;

public class HotelRoom
{
    public Guid Id { get; set; }
    public Guid HotelId { get; set; }
    public byte NumberOfResidents { get; set; }
    public string Description { get; set; } = null!;
    public decimal Price { get; set; }
    
    public IEnumerable<RoomState> States { get; set; } = [];
    public IEnumerable<RoomPhoto> Photos { get; set; } = [];
}