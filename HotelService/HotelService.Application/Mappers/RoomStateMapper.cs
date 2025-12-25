using HotelService.Application.Models.RoomStates;
using HotelService.Domain.Entities;

namespace HotelService.Application.Mappers;

public static class RoomStateMapper
{
    public static RoomStateDto ToRoomStateDto(this RoomState roomState)
    {
        return new RoomStateDto(
            roomState.Id,
            roomState.HotelId,
            roomState.StartDate,
            roomState.EndDate);
    }

    public static RoomState ToRoomState(this AddRoomStateRequest roomState, Guid roomId, Guid hotelId, string personId)
    {
        return new RoomState
        {
            Id = roomId,
            PersonId = personId,
            HotelId = hotelId,
            StartDate = roomState.StartDate,
            EndDate = roomState.EndDate
        };
    }
}