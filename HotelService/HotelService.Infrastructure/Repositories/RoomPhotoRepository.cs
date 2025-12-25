using HotelService.Application.Interfaces.Repositories;
using HotelService.Domain.Entities;
using HotelService.Infrastructure.Core;
using Microsoft.EntityFrameworkCore;

namespace HotelService.Infrastructure.Repositories;

public class RoomPhotoRepository(
    ApplicationDbContext context)
    : IRoomPhotoRepository
{
    public async Task<IEnumerable<RoomPhoto>> GetAllAsync(CancellationToken ct)
    {
        return await context.RoomPhotos.ToListAsync(ct);
    }

    public async Task<IEnumerable<RoomPhoto>> GetByHotelIdAsync(Guid hotelId, CancellationToken ct)
    {
        return await context.RoomPhotos.Where(r => r.HotelId == hotelId).ToListAsync(ct);
    }

    public async Task<IEnumerable<RoomPhoto>> GetByRoomIdAsync(Guid roomId, CancellationToken ct)
    {
        return await context.RoomPhotos.Where(r => r.RoomId == roomId).ToListAsync(ct);
    }

    public async Task<RoomPhoto?> GetFirstByRoomIdAsync(Guid roomId, CancellationToken ct)
    {
        return await context.RoomPhotos.FirstOrDefaultAsync(r => r.RoomId == roomId, ct);
    }

    public async Task<RoomPhoto?> GetByIdAsync(Guid roomPhotoId, CancellationToken ct)
    {
        return await context.RoomPhotos.FirstOrDefaultAsync(r => r.HotelId == roomPhotoId, ct);
    }

    public async Task<bool> AddAsync(RoomPhoto hotelPhoto, CancellationToken ct)
    {
        await context.RoomPhotos.AddAsync(hotelPhoto, ct);
        var rows = await  context.SaveChangesAsync(ct);
        
        return rows > 0;
    }

    public async Task<bool> DeleteAsync(Guid roomPhotoId, CancellationToken ct)
    {
        var photo = await context.RoomPhotos.FirstOrDefaultAsync(r => r.HotelId == roomPhotoId, ct);
        if  (photo == null) return false;

        context.RoomPhotos.Remove(photo);
        var rows = await context.SaveChangesAsync(ct);
        
        return rows > 0;
    }

    public void DeleteByRoomId(Guid roomId, CancellationToken ct)
    {
        var photos = context.RoomPhotos.Where(r => r.RoomId == roomId);
        context.RoomPhotos.RemoveRange(photos);
    }

    public void DeleteByHotelId(Guid hotelId, CancellationToken ct)
    {
        var photos = context.RoomPhotos.Where(r => r.HotelId == hotelId);
        context.RoomPhotos.RemoveRange(photos);
    }
}