using HotelService.Domain.Entities;

namespace HotelService.Application.Interfaces.Repositories;

public interface IRoomPhotoRepository
{
    Task<IEnumerable<RoomPhoto>> GetAllAsync(CancellationToken ct);
    
    Task<IEnumerable<RoomPhoto>> GetByHotelIdAsync(Guid hotelId, CancellationToken ct);
    Task<IEnumerable<RoomPhoto>> GetByRoomIdAsync(Guid roomId, CancellationToken ct);
    Task<RoomPhoto?> GetFirstByRoomIdAsync(Guid roomId, CancellationToken ct);
    Task<RoomPhoto?> GetByIdAsync(Guid hotelPhotoId, CancellationToken ct);
    Task<bool> AddAsync(RoomPhoto hotelPhoto, CancellationToken ct);
    Task<bool> DeleteAsync(Guid hotelPhotoId, CancellationToken ct);
    Task<bool> DeleteByRoomIdAsync(Guid roomId, CancellationToken ct);
    Task<bool> DeleteByHotelIdAsync(Guid hotelId, CancellationToken ct);
}