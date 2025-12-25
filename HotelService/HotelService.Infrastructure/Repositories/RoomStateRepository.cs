using HotelService.Application.Interfaces.Repositories;
using HotelService.Domain.Entities;
using HotelService.Infrastructure.Core;
using Microsoft.EntityFrameworkCore;

namespace HotelService.Infrastructure.Repositories;

public class RoomStateRepository(
    ApplicationDbContext context)
    : IRoomStateRepository
{
    public async Task<IEnumerable<RoomState>> GetAllAsync(CancellationToken ct)
    {
        return await context.RoomStates.ToListAsync(ct);
    }

    public async Task<RoomState?> GetByIdAsync(Guid roomStateId, CancellationToken ct)
    {
        return await context.RoomStates.FirstOrDefaultAsync(r => r.Id == roomStateId, ct);
    }

    public async Task<IEnumerable<RoomState>> GetByRoomIdAsync(Guid roomId, CancellationToken ct)
    {
        return await context.RoomStates.Where(r => r.RoomId == roomId).ToListAsync(ct);
    }

    public async Task<IEnumerable<RoomState>> GetRoomStatesByDateAsync(DateOnly start, DateOnly end, CancellationToken ct)
    {
        return await context.RoomStates.Where(r => r.EndDate >= start && r.StartDate <= end).ToListAsync(ct);
    }

    public async Task<bool> AddAsync(RoomState roomState, CancellationToken ct)
    {
        await context.RoomStates.AddAsync(roomState, ct);
        var rows =  await context.SaveChangesAsync(ct);
        
        return rows > 0;
    }

    public async Task<bool> DeleteAsync(Guid roomStateId, CancellationToken ct)
    {
        var state = await context.RoomStates.FirstOrDefaultAsync(r => r.Id == roomStateId, ct);
        if (state == null) return false;
        
        context.RoomStates.Remove(state);
        var rows = await context.SaveChangesAsync(ct);
        
        return rows > 0;
    }

    public void DeleteByRoomId(Guid roomId, CancellationToken ct)
    {
        var states = context.RoomStates.Where(r => r.RoomId == roomId);
        context.RoomStates.RemoveRange(states);
    }

    public void DeleteByHotelId(Guid hotelId, CancellationToken ct)
    {
        var states = context.RoomStates.Where(r => r.HotelId == hotelId);
        context.RoomStates.RemoveRange(states);
    }
}