namespace HotelService.Application.Models.Hotels;

public record AddHotelRequest(
    string Name,
    string Country,
    string City,
    byte Stars,
    string Description);