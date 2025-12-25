using HotelService.Domain.Entities;

namespace HotelService.Application.Interfaces.Repositories;

public interface IRoomPhotoRepository
{
    Task<IEnumerable<RoomPhoto>> GetAllAsync(CancellationToken ct);
    Task<IEnumerable<RoomPhoto>> GetByHotelIdAsync(Guid hotelId, CancellationToken ct);
    Task<IEnumerable<RoomPhoto>> GetByRoomIdAsync(Guid roomId, CancellationToken ct);
    Task<RoomPhoto?> GetFirstByRoomIdAsync(Guid roomId, CancellationToken ct);
    Task<RoomPhoto?> GetByIdAsync(Guid roomPhotoId, CancellationToken ct);
    Task<bool> AddAsync(RoomPhoto hotelPhoto, CancellationToken ct);
    Task<bool> DeleteAsync(Guid roomPhotoId, CancellationToken ct);
    void DeleteByRoomId(Guid roomId, CancellationToken ct);
    void DeleteByHotelId(Guid hotelId, CancellationToken ct);
}