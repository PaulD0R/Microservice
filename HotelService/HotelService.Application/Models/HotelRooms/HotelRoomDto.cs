using HotelService.Application.Models.RoomPhotos;

namespace HotelService.Application.Models.HotelRooms;

public record HotelRoomDto(
    Guid Id,
    Guid HotelId,
    byte NumberOfResidents,
    decimal Price,
    RoomPhotoDto Photo);