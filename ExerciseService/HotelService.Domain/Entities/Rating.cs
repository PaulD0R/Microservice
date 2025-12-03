namespace HotelService.Domain.Entities;

public class Rating
{
    public Guid Id { get; set; }
    public Guid HotelId { get; set; }
    public string PersonId { get; set; } = null!;
    public byte Value { get; set; }
}