namespace HotelService.Domain.Entities;

public class RoomState
{
    public Guid Id;
    public Guid RoomId { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
}