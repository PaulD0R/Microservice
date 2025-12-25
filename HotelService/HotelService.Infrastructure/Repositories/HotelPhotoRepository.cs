using HotelService.Application.Interfaces.Repositories;
using HotelService.Domain.Entities;
using HotelService.Infrastructure.Core;
using Microsoft.EntityFrameworkCore;

namespace HotelService.Infrastructure.Repositories;

public class HotelPhotoRepository(
    ApplicationDbContext context)
    : IHotelPhotoRepository
{
    public async Task<IEnumerable<HotelPhoto>> GetAllAsync(CancellationToken ct)
    {
        return await context.HotelPhotos.ToListAsync(ct);
    }

    public async Task<IEnumerable<HotelPhoto>> GetByHotelIdAsync(Guid hotelId, CancellationToken ct)
    {
        return await context.HotelPhotos.Where(h => h.HotelId == hotelId).ToListAsync(ct);
    }

    public async Task<HotelPhoto?> GetFirstByHotelIdAsync(Guid hotelId, CancellationToken ct)
    {
        return await context.HotelPhotos.FirstOrDefaultAsync(h => h.HotelId == hotelId, ct);
    }

    public async Task<HotelPhoto?> GetByIdAsync(Guid hotelPhotoId, CancellationToken ct)
    {
        return await context.HotelPhotos.FirstOrDefaultAsync(h => h.HotelId == hotelPhotoId, ct);
    }

    public async Task<bool> AddAsync(HotelPhoto hotelPhoto, CancellationToken ct)
    {
        await context.HotelPhotos.AddAsync(hotelPhoto, ct);
        var rows = await context.SaveChangesAsync(ct);
        
        return rows > 0;
    }

    public async Task<bool> DeleteAsync(Guid hotelPhotoId, CancellationToken ct)
    {
        var photo = await context.HotelPhotos.FirstOrDefaultAsync(h => h.HotelId == hotelPhotoId, ct);
        if (photo == null) return false;
        
        context.HotelPhotos.Remove(photo);
        var rows = await context.SaveChangesAsync(ct);
        
        return rows > 0;
    }

    public void DeleteByHotelId(Guid hotelId, CancellationToken ct)
    {
        var photos = context.HotelPhotos.Where(h => h.HotelId == hotelId);
        context.HotelPhotos.RemoveRange(photos);
    }
}