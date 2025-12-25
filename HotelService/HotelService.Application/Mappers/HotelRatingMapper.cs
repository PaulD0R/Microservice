using HotelService.Application.Models.HotelRating;
using HotelService.Domain.Entities;

namespace HotelService.Application.Mappers;

public static class HotelRatingMapper
{
    public static HotelRating ToHotelRating(this HotelRatingRequest rating, Guid hotelId, string personId)
    {
        return new HotelRating
        {
            HotelId = hotelId,
            PersonId = personId,
            Value = rating.Value
        };
    }
}