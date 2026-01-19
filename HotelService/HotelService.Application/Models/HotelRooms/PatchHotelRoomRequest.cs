namespace HotelService.Application.Models.HotelRooms;

public record PatchHotelRoomRequest(  
    Guid HotelId,
    byte? NumberOfResidents,
    decimal? Price,
    string? Description);