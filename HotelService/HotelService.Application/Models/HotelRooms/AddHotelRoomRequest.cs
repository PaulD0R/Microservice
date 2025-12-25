namespace HotelService.Application.Models.HotelRooms;

public record AddHotelRoomRequest(
    Guid HotelId,
    byte NumberOfResidents,
    string Description);