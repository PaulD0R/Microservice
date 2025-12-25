using HotelService.Domain.Entities;

namespace HotelService.Application.Interfaces.Repositories;

public interface IHotelCommentRepository
{
    Task<IEnumerable<HotelComment>> GetAllAsync(CancellationToken ct);
    Task<IEnumerable<HotelComment>> GetByHotelIdAsync(Guid hotelId, CancellationToken ct);
    Task<HotelComment?> GetByIdAsync(Guid commentId, CancellationToken ct);
    Task<bool> AddAsync(HotelComment hotelComment, CancellationToken ct);
    Task<bool> DeleteAsync(Guid commentId, CancellationToken ct);
    void DeleteByHotelIdAsync(Guid hotelId, CancellationToken ct);
}