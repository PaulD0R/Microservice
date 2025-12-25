using HotelService.Domain.Entities;

namespace HotelService.Application.Interfaces.Repositories;

public interface IHotelRatingRepository
{
    Task<IEnumerable<HotelRating>> GetAllAsync(CancellationToken ct);
    Task<IEnumerable<HotelRating>> GetByHotelIdAsync(Guid hotelId, CancellationToken ct);
    Task<IEnumerable<HotelRating>> GetByPersonIdAsync(string personId, CancellationToken ct);
    Task<HotelRating?> GetByHotelIdAndPersonIdAsync(Guid hotelId, string personId, CancellationToken ct);
    Task<bool> AddAsync(HotelRating hotelRating, CancellationToken ct);
    Task<bool> ChangeAsync(HotelRating hotelRating, CancellationToken ct);
    Task<bool> DeleteAsync(Guid hotelId, string personId, CancellationToken ct);
    void DeleteByHotelId(Guid hotelId, CancellationToken ct);
}