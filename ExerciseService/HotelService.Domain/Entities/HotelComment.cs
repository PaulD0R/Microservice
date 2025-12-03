namespace HotelService.Domain.Entities;

public class HotelComment
{
    public Guid Id { get; set; }
    public Guid HotelId { get; set; }
    public string PersonId { get; set; } = null!;
    public string Message { get; set; } = null!;
}