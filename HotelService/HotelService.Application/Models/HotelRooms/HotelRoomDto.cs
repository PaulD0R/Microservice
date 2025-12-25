using HotelService.Application.Models.RoomPhotos;

namespace HotelService.Application.Models.HotelRooms;

public record HotelRoomDto(
    Guid Id,
    Guid HotelId,
    byte NumberOfResidents,
    RoomPhotoDto Photo);