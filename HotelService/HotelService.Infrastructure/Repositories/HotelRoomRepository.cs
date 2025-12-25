using HotelService.Application.Interfaces.Repositories;
using HotelService.Domain.Entities;
using HotelService.Infrastructure.Core;
using Microsoft.EntityFrameworkCore;

namespace HotelService.Infrastructure.Repositories;

public class HotelRoomRepository(
    ApplicationDbContext context)
    : IHotelRoomRepository
{
    public async Task<IEnumerable<HotelRoom>> GetAllAsync(CancellationToken ct)
    {
        return await context.HotelRooms.ToListAsync(ct);
    }

    public async Task<IEnumerable<HotelRoom>> GetByHotelIdAsync(Guid hotelId, CancellationToken ct)
    {
        return await context.HotelRooms.Where(h => h.HotelId == hotelId).ToListAsync(ct);
    }

    public async Task<HotelRoom?> GetByIdAsync(Guid hotelRoomId, CancellationToken ct)
    {
        return await  context.HotelRooms.FirstOrDefaultAsync(h => h.HotelId == hotelRoomId, ct);
    }

    public async Task<bool> AddAsync(HotelRoom hotelRoom, CancellationToken ct)
    {
        await  context.HotelRooms.AddAsync(hotelRoom, ct);
        var rows = await context.SaveChangesAsync(ct);
        
        return rows > 0;
    }

    public async Task<bool> UpdateAsync(HotelRoom hotelRoom, CancellationToken ct)
    {
        context.HotelRooms.Update(hotelRoom);
        var rows = await context.SaveChangesAsync(ct);
        
        return rows > 0;
    }

    public async Task<bool> DeleteAsync(Guid hotelRoomId, CancellationToken ct)
    {
        var room = await context.HotelRooms.FirstOrDefaultAsync(h => h.HotelId == hotelRoomId, ct);
        if (room == null) return false;
        
        var rows = await context.SaveChangesAsync(ct);
        
        return rows > 0;
    }

    public void DeleteByHotelId(Guid hotelId, CancellationToken ct)
    {
        var rooms = context.HotelRooms.Where(h => h.HotelId == hotelId);
        context.HotelRooms.RemoveRange(rooms);  
    }
}