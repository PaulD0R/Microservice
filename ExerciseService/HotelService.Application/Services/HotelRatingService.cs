using HotelService.Application.Interfaces.Caches;
using HotelService.Application.Interfaces.Repositories;
using HotelService.Application.Interfaces.Services;
using HotelService.Application.Mappers;
using HotelService.Application.Models.HotelRating;
using HotelService.Domain.Exceptions;
using HotelService.Domain.Interfaces.Services;

namespace HotelService.Application.Services;

public class HotelRatingService(
    IHotelRatingRepository hotelRatingRepository,
    ICacheService cacheService,
    IRatingCalculateService ratingCalculateService)
    : IHotelRatingService
{
    public async Task<decimal> GetHotelRatingByHotelIdAsync(Guid hotelId, CancellationToken ct)
    {
        var rating = await cacheService.GetAsync<decimal?>($"rating_{hotelId}", ct);
        if (rating != null) return (decimal)rating;
        
        var ratings = await hotelRatingRepository.GetByHotelIdAsync(hotelId, ct);
        rating = ratingCalculateService.GetShownRating(ratings);

        return (decimal)rating;
    }

    public async Task<bool> ChangeHotelRatingAsync(
        HotelRatingRequest hotelRatingRequest, 
        string personId, Guid hotelId, 
        CancellationToken ct)
    {
        var rating = hotelRatingRequest.ToHotelRating(hotelId, personId);
        return await hotelRatingRepository.ChangeAsync(rating, ct) ? true :
                throw  new NotFoundException("Hotel or Person Not Found");;
    }

    public async Task<bool> DeleteHotelRatingAsync(Guid id, CancellationToken ct)
    {
        return await hotelRatingRepository.DeleteByHotelIdAsync(id, ct) ? true :
                throw new NotFoundException("Hotel Rating Not Found");
    }

    public async Task<bool> DeleteHotelRatingsByHotelIdAsync(Guid hotelId, CancellationToken ct)
    {
        return await hotelRatingRepository.DeleteByHotelIdAsync(hotelId, ct) ? true :
                throw  new NotFoundException("Hotel Not Found");
    }
}