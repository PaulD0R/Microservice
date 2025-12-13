using HotelService.Application.Models.RoomStates;
using HotelService.Domain.Entities;

namespace HotelService.Application.Interfaces.Services;

public interface IRoomStateService
{
    Task<IEnumerable<RoomStateDto>> GetRoomStateByHotelRoomIdAsync(Guid roomId, CancellationToken ct);
    Task<bool> AddRoomStateAsync(AddRoomStateRequest roomStateRequest, Guid roomId, CancellationToken ct);
    Task<bool> DeleteRoomStateAsync(Guid id, CancellationToken ct);
    Task<bool> DeleteRoomStatesByHotelRoomIdAsync(Guid roomId, CancellationToken ct);
    Task<bool> DeleteRoomStatesByHotelIdAsync(Guid hotelId, CancellationToken ct);
}