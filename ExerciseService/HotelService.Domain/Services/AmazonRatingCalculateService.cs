using HotelService.Domain.Entities;
using HotelService.Domain.Interfaces.Services;

namespace HotelService.Domain.Services;

public class AmazonRatingCalculateService : IRatingCalculateService
{
    public decimal GetRealRating(IEnumerable<HotelRating> ratings)
    {
        throw new NotImplementedException();
    }

    public decimal GetShownRating(IEnumerable<HotelRating> ratings)
    {
        throw new NotImplementedException();
    }
}