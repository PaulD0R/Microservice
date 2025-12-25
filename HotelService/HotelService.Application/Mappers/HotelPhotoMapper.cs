using HotelService.Application.Models.HotelPhotos;
using HotelService.Domain.Entities;

namespace HotelService.Application.Mappers;

public static class HotelPhotoMapper
{
    public static HotelPhotoDto ToHotelPhotoDto(this HotelPhoto hotelPhoto, string key)
    {
        return new HotelPhotoDto(
            hotelPhoto.Name,
            key);
    }

    public static HotelPhoto ToHotelPhoto(this AddHotelPhotoRequest addHotelPhotoRequest, string key, Guid hotelId)
    {
        return new HotelPhoto
        {
            HotelId = hotelId,
            Name = addHotelPhotoRequest.Name,
            Key = key,
            Type = addHotelPhotoRequest.Type
        };
    }
}