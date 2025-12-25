namespace HotelService.Application.Models.Hotels;

public record PatchHotelRequest(
    string? Name,
    string? Country,
    string? City,
    byte? Stars,
    string? Description);