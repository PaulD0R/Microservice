using HotelService.Application.Interfaces.Repositories;
using HotelService.Domain.Entities;
using HotelService.Infrastructure.Core;
using Microsoft.EntityFrameworkCore;

namespace HotelService.Infrastructure.Repositories;

public class HotelCommentRepository(
    ApplicationDbContext context)
    : IHotelCommentRepository
{
    public async Task<IEnumerable<HotelComment>> GetAllAsync(CancellationToken ct)
    {
        return await context.HotelComments.ToListAsync(ct);
    }

    public async Task<IEnumerable<HotelComment>> GetByHotelIdAsync(Guid hotelId, CancellationToken ct)
    {
        return await context.HotelComments.Where(h => h.HotelId == hotelId).ToListAsync(ct);
    }

    public async Task<HotelComment?> GetByIdAsync(Guid commentId, CancellationToken ct)
    {
        return await context.HotelComments.FirstOrDefaultAsync(h => h.Id == commentId, ct);
    }

    public async Task<bool> AddAsync(HotelComment hotelComment, CancellationToken ct)
    {
        var result = await  context.HotelComments.AddAsync(hotelComment, ct);
        var rowsAffected = await context.SaveChangesAsync(ct);
        
        return rowsAffected > 0;
    }

    public async Task<bool> DeleteAsync(Guid commentId, CancellationToken ct)
    {
        var comment  = await context.HotelComments.FirstOrDefaultAsync(h => h.Id == commentId, ct);
        if (comment == null) return false;

        var result = context.HotelComments.Remove(comment);
        var rowsAffected = await context.SaveChangesAsync(ct);
        
        return rowsAffected > 0;
    }

    public void DeleteByHotelIdAsync(Guid hotelId, CancellationToken ct)
    {
        var comments = context.HotelComments.Where(h => h.HotelId == hotelId);
        context.HotelComments.RemoveRange(comments);
    }
}