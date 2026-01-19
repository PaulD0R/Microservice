using HotelService.Application.Models.HotelPhotos;

namespace HotelService.Application.Models.Hotels;

public record HotelDto(
    Guid Id,
    string Name,
    string Country,
    string City,
    byte Stars,
    decimal MinPrice,
    HotelPhotoDto Photo);