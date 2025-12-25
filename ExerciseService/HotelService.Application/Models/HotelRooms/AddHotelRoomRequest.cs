namespace HotelService.Application.Models.HotelRooms;

public record AddHotelRoomRequest(
    byte NumberOfResidents,
    string Description);