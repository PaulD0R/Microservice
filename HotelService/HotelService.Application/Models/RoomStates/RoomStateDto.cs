namespace HotelService.Application.Models.RoomStates;

public record RoomStateDto(
    Guid Id,
    Guid RoomId,
    DateOnly StartDate,
    DateOnly EndDate);