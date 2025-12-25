using HotelService.Application.Models.RoomStates;

namespace HotelService.Application.Interfaces.Services;

public interface IRoomStateService
{
    Task<IEnumerable<RoomStateDto>> GetRoomStateByHotelRoomIdAsync(Guid roomId, CancellationToken ct);
    Task<bool> AddRoomStateAsync(
        AddRoomStateRequest roomStateRequest,
        Guid roomId, 
        Guid hotelId,
        string personId, 
        CancellationToken ct);
    Task<bool> DeleteRoomStateAsync(Guid id, CancellationToken ct);
    void DeleteRoomStatesByHotelRoomId(Guid roomId, CancellationToken ct);
    void DeleteRoomStatesByHotelId(Guid hotelId, CancellationToken ct);
}