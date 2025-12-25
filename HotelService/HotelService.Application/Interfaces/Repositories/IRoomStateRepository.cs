using HotelService.Domain.Entities;

namespace HotelService.Application.Interfaces.Repositories;

public interface IRoomStateRepository
{
    Task<IEnumerable<RoomState>> GetAllAsync(CancellationToken ct);
    Task<RoomState?> GetByIdAsync(Guid roomStateId, CancellationToken ct);
    Task<IEnumerable<RoomState>> GetByRoomIdAsync(Guid roomId, CancellationToken ct);
    Task<IEnumerable<RoomState>> GetRoomStatesByDateAsync(DateOnly start, DateOnly end, CancellationToken ct);
    Task<bool> AddAsync(RoomState roomState, CancellationToken ct);
    Task<bool> DeleteAsync(Guid roomStateId, CancellationToken ct);
    void DeleteByRoomId(Guid roomId, CancellationToken ct);
    void DeleteByHotelId(Guid hotelId, CancellationToken ct);
}