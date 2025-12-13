namespace HotelService.Application.Models.RoomStates;

public record RoomStateDto(
    Guid Id,
    DateOnly StartDate,
    DateOnly EndDate);