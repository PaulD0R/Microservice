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

    public static RoomPhoto ToRoomPhoto(this AddRoomPhotoRequest addRoomPhotoRequest,
        string key, Guid roomId, Guid hotelId)
    {
        return new RoomPhoto
        {
            RoomId = roomId,
            HotelId = hotelId,
            Name = addRoomPhotoRequest.Name,
            Key = key,
            Type = addRoomPhotoRequest.Type
        };
    }
}