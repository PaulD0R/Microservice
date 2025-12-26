using HotelService.Application.Interfaces.Caches;
using HotelService.Application.Interfaces.Repositories;
using HotelService.Application.Interfaces.Services;
using HotelService.Application.Mappers;
using HotelService.Application.Models.HotelRating;
using HotelService.Domain.Entities;
using HotelService.Domain.Exceptions;
using HotelService.Domain.Interfaces.Services;

namespace HotelService.Application.Services;

public class HotelRatingService(
    IHotelRatingRepository hotelRatingRepository,
    ICacheService cacheService,
    IRatingCalculateService ratingCalculateService)
    : IHotelRatingService
{
    private const string HotelRatingCacheKey = "hotelRatingCache_";
    
    public async Task<decimal> GetHotelRatingByHotelIdAsync(Guid hotelId, CancellationToken ct)
    {
        var key = $"{HotelRatingCacheKey}{hotelId}";
        
        var rating = await cacheService.GetAsync<decimal?>(key, ct);
        if (rating != null) return (decimal)rating;
        
        var ratings = await hotelRatingRepository.GetByHotelIdAsync(hotelId, ct);
        rating = ratingCalculateService.GetShownRating(ratings.ToList());

        await cacheService.SetAsync(key, rating, ct);
        
        return (decimal)rating;
    }

    public async Task<bool> ChangeHotelRatingAsync(
        HotelRatingRequest hotelRatingRequest, 
        string personId, Guid hotelId, 
        CancellationToken ct)
    {
        var rating = hotelRatingRequest.ToHotelRating(hotelId, personId);
        var checkRating = await hotelRatingRepository.GetByHotelIdAndPersonIdAsync(hotelId, personId, ct);
        
        Func<HotelRating, CancellationToken, Task<bool>> operation = 
            checkRating == null ? hotelRatingRepository.AddAsync : hotelRatingRepository.ChangeAsync;
        
        return await operation(rating, ct) ? true :
            throw new NotFoundException("Hotel or Person Not Found");
    }

    public async Task<bool> DeleteHotelRatingAsync(Guid hotelId, string personId, CancellationToken ct)
    {
        return await hotelRatingRepository.DeleteAsync(hotelId, personId, ct) ? true :
                throw new NotFoundException("Hotel Rating Not Found");
    }

    public async Task<bool> DeleteHotelRatingsByHotelIdAsync(Guid hotelId, CancellationToken ct)
    {
        await cacheService.RemoveAsync($"{HotelRatingCacheKey}{hotelId}", ct);
        hotelRatingRepository.DeleteByHotelId(hotelId, ct);
        
        return true;
    }
}