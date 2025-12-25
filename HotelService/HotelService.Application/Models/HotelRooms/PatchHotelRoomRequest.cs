namespace HotelService.Application.Models.HotelRooms;

public record PatchHotelRoomRequest(  
    Guid HotelId,
    byte? NumberOfResidents,
    string? Description);