using HotelService.Application.Models.RoomPhotos;

namespace HotelService.Application.Models.HotelRooms;

public record HotelRoomFullDto(
    Guid Id,
    Guid HotelId,
    byte NumberOfResidents,
    string Description,
    IEnumerable<RoomPhotoDto> Photos);