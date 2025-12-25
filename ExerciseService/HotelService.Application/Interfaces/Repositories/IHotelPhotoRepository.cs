using HotelService.Domain.Entities;

namespace HotelService.Application.Interfaces.Repositories;

public interface IHotelPhotoRepository
{
    Task<IEnumerable<HotelPhoto>> GetAllAsync(CancellationToken ct);
    Task<IEnumerable<HotelPhoto>> GetByHotelIdAsync(Guid hotelId, CancellationToken ct);
    Task<HotelPhoto?> GetFirstByHotelIdAsync(Guid hotelId, CancellationToken ct);
    Task<HotelPhoto?> GetByIdAsync(Guid hotelPhotoId, CancellationToken ct);
    Task<bool> AddAsync(HotelPhoto hotelPhoto, CancellationToken ct);
    Task<bool> DeleteAsync(Guid hotelPhotoId, CancellationToken ct);
    Task<bool> DeleteByHotelIdAsync(Guid hotelId, CancellationToken ct);
}