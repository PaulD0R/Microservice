using HotelService.Domain.Entities;

namespace HotelService.Application.Interfaces.Repositories;

public interface IHotelRatingRepository
{
    Task<IEnumerable<HotelRating>> GetAllAsync(CancellationToken ct);
    Task<IEnumerable<HotelRating>> GetByHotelIdAsync(Guid hotelId, CancellationToken ct);
    Task<IEnumerable<HotelRating>> GetByPersonIdAsync(Guid personId, CancellationToken ct);
    Task<bool> ChangeAsync(HotelRating hotelRating, CancellationToken ct);
    Task<Hotel> DeleteAsync(Guid ratingId, CancellationToken ct);
    Task<bool> DeleteByHotelIdAsync(Guid hotelId, CancellationToken ct);
}