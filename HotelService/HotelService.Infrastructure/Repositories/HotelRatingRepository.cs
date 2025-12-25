using HotelService.Application.Interfaces.Repositories;
using HotelService.Domain.Entities;
using HotelService.Infrastructure.Core;
using Microsoft.EntityFrameworkCore;

namespace HotelService.Infrastructure.Repositories;

public class HotelRatingRepository(
    ApplicationDbContext context)
    : IHotelRatingRepository
{
    public async Task<IEnumerable<HotelRating>> GetAllAsync(CancellationToken ct)
    {
        return await context.HotelRatings.ToListAsync(ct);
    }

    public async Task<IEnumerable<HotelRating>> GetByHotelIdAsync(Guid hotelId, CancellationToken ct)
    {
        return await context.HotelRatings.Where(h => h.HotelId == hotelId).ToListAsync(ct);
    }

    public async Task<IEnumerable<HotelRating>> GetByPersonIdAsync(string personId, CancellationToken ct)
    {
        return await context.HotelRatings.Where(h => h.PersonId == personId).ToListAsync(ct);
    }

    public async Task<HotelRating?> GetByHotelIdAndPersonIdAsync(Guid hotelId, string personId, CancellationToken ct)
    {
        return await context.HotelRatings.
            FirstOrDefaultAsync(h => h.HotelId == hotelId && h.PersonId == personId, ct);
    }

    public async Task<bool> AddAsync(HotelRating hotelRating, CancellationToken ct)
    {
        await context.HotelRatings.AddAsync(hotelRating, ct);
        var rows = await context.SaveChangesAsync(ct);
        
        return rows > 0;
    }

    public async Task<bool> ChangeAsync(HotelRating hotelRating, CancellationToken ct)
    {
        context.HotelRatings.Update(hotelRating);
        var rowsAffected = await context.SaveChangesAsync(ct);
        
        return rowsAffected > 0;
    }

    public async Task<bool> DeleteAsync(Guid hotelId, string personId, CancellationToken ct)
    {
        var rating = await context.HotelRatings
            .FirstOrDefaultAsync(h => h.PersonId == personId && h.HotelId == hotelId, ct);
        if (rating == null) return false;
        
        context.HotelRatings.Remove(rating);
        await context.SaveChangesAsync(ct);

        return true;
    }

    public void DeleteByHotelId(Guid hotelId, CancellationToken ct)
    {
        var ratings = context.HotelRatings.Where(h => h.HotelId == hotelId);
        context.HotelRatings.RemoveRange(ratings);
    }
}