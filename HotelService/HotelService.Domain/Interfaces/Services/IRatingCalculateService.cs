using HotelService.Domain.Entities;

namespace HotelService.Domain.Interfaces.Services;

public interface IRatingCalculateService
{
    public decimal GetRealRating(IEnumerable<HotelRating> ratings);

    public decimal GetShownRating(IEnumerable<HotelRating> ratings) => GetRealRating(ratings);
}