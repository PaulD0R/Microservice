using HotelService.Domain.Entities;

namespace HotelService.Domain.Interfaces.Services;

public interface IRatingCalculateService
{
    public decimal GetRealRating(ICollection<HotelRating> ratings);

    public decimal GetShownRating(ICollection<HotelRating> ratings) => GetRealRating(ratings);
}