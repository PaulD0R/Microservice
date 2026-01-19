using HotelService.Application.Helpers.FilterModels;
using HotelService.Application.Helpers.HelperExtensions;
using HotelService.Application.Helpers.SortModels;
using HotelService.Application.Interfaces.Repositories;
using HotelService.Domain.Entities;
using HotelService.Infrastructure.Core;
using Microsoft.EntityFrameworkCore;

namespace HotelService.Infrastructure.Repositories;

public class HotelRepository(
    ApplicationDbContext context)
    : IHotelRepository
{
    public async Task<IEnumerable<Hotel>> GetAllAsync(CancellationToken ct)
    {
        return await context.Hotels.ToListAsync(ct);
    }

    public async Task<IEnumerable<Hotel>> GetByPageAsync(
        int pageNumber,
        int pageSize,
        HotelFilterModel hotelFilterModel,
        HotelSortModel hotelSortModel,
        CancellationToken ct)
    {
        return await context.Hotels.Filter(hotelFilterModel).Order(hotelSortModel)
            .Skip((pageNumber - 1) * pageSize).Take(pageSize)
            .ToListAsync(ct);
    }

    public async Task<Hotel?> GetByIdAsync(Guid hotelId, CancellationToken ct)
    {
        return await  context.Hotels.FirstOrDefaultAsync(h => h.Id == hotelId, ct);
    }

    public async Task<bool> UpdateAsync(Hotel hotel, CancellationToken ct)
    {
        context.Hotels.Update(hotel);
        var rowsAffected = await context.SaveChangesAsync(ct);
        
        return rowsAffected > 0;
    }

    public async Task<bool> AddAsync(Hotel hotel, CancellationToken ct)
    {
        await context.Hotels.AddAsync(hotel, ct);
        var rowsAffected = await context.SaveChangesAsync(ct);
        
        return rowsAffected > 0;
    }

    public async Task<bool> DeleteAsync(Guid hotelId, CancellationToken ct)
    {
        var hotel = await context.Hotels.FirstOrDefaultAsync(h => h.Id == hotelId, ct);
        if (hotel == null) return false;
        
        context.Hotels.Remove(hotel);
        await context.SaveChangesAsync(ct);
        return true;
    }
}