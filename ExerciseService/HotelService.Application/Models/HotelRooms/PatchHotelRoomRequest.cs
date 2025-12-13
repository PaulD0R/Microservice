namespace HotelService.Application.Models.HotelRooms;

public record PatchHotelRoomRequest(    
    byte? NumberOfResidents,
    string? Description);