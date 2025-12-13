using HotelService.Application.Models.HotelPhotos;
using HotelService.Domain.Entities;

namespace HotelService.Application.Mappers;

public static class HotelPhotoMapper
{
    public static HotelPhotoDto ToHotelPhotoDto(this HotelPhoto hotelPhoto, string key)
    {
        return new HotelPhotoDto(
            hotelPhoto.Name,
            hotelPhoto.Key);
    }

    public static HotelPhoto ToHotelPhoto(this AddHotelPhotoRequest addHotelPhotoRequest, string key)
    {
        return new HotelPhoto
        {
            Name = addHotelPhotoRequest.Name,
            Key = key,
            Type = addHotelPhotoRequest.Type
        };
    }
}