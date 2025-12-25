using HotelService.Application.Models.RoomStates;
using HotelService.Domain.Entities;

namespace HotelService.Application.Mappers;

public static class RoomStateMapper
{
    public static RoomStateDto ToRoomStateDto(this RoomState roomState)
    {
        return new RoomStateDto(
            roomState.Id,
            roomState.StartDate,
            roomState.EndDate);
    }

    public static RoomState ToRoomState(this AddRoomStateRequest roomState, Guid roomId)
    {
        return new RoomState
        {
            Id = roomId,
            StartDate = roomState.StartDate,
            EndDate = roomState.EndDate
        };
    }
}