namespace HotelService.Application.Models.RoomStates;

public record AddRoomStateRequest(
    DateOnly StartDate,
    DateOnly EndDate);