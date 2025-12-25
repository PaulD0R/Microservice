using HotelService.Application.Models.RoomPhotos;

namespace HotelService.Application.Models.HotelRooms;

public record HotelRoomFullDto(
    Guid Id,
    byte NumberOfResidents,
    string Description,
    IEnumerable<RoomPhotoDto> Photos);