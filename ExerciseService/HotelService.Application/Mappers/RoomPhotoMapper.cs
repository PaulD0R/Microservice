using HotelService.Application.Models.RoomPhotos;
using HotelService.Domain.Entities;

namespace HotelService.Application.Mappers;

public static class RoomPhotoMapper
{
    public static RoomPhotoDto ToRoomPhotoDto(this RoomPhoto roomPhoto, string key)
    {
        return new RoomPhotoDto(
            roomPhoto.Name,
            key
        );
    }

    public static RoomPhoto ToRoomPhoto(this AddRoomPhotoRequest addRoomPhotoRequest, string key)
    {
        return new RoomPhoto
        {
            Name = addRoomPhotoRequest.Name,
            Key = key,
            Type = addRoomPhotoRequest.Type
        };
    }
}