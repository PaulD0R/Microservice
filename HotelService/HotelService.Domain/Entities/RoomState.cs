namespace HotelService.Domain.Entities;

public class RoomState
{
    public Guid Id { get; set; }
    public Guid RoomId { get; set; }
    public Guid HotelId { get; set; }
    public string PersonId { get; set; } = null!;
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
}