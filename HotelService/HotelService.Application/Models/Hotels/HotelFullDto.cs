using HotelService.Application.Models.HotelPhotos;

namespace HotelService.Application.Models.Hotels;

public record HotelFullDto(
    Guid Id,
    string Name,
    string Country,
    string City,
    string FullAddress,
    double Latitude,
    double Longitude,
    byte Stars,
    string Description,
    decimal Rating,
    IEnumerable<HotelPhotoDto> Photo);