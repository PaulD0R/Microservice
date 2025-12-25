using HotelService.Application.Models.HotelRating;

namespace HotelService.Application.Interfaces.Services;

public interface IHotelRatingService
{
    Task<decimal> GetHotelRatingByHotelIdAsync(Guid hotelId, CancellationToken ct);
    Task<bool> ChangeHotelRatingAsync(
        HotelRatingRequest hotelRatingRequest,
        string personId,
        Guid hotelId,
        CancellationToken ct);
    Task<bool> DeleteHotelRatingAsync(Guid id, CancellationToken ct);
    Task<bool> DeleteHotelRatingsByHotelIdAsync(Guid hotelId, CancellationToken ct);
}