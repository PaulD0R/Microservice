using HotelService.Application.Models.HotelComments;

namespace HotelService.Application.Interfaces.Services;

public interface IHotelCommentService
{
    Task<IEnumerable<HotelCommentDto>> GetHotelCommentsByHotelIdAsync(Guid hotelId, CancellationToken ct);
    Task<bool> AddHotelCommentAsync(
        AddHotelCommentRequest hotelComment, 
        string personId, 
        Guid hotelId, 
        CancellationToken ct);
    Task<bool> DeleteHotelCommentAsync(Guid id, CancellationToken ct);
    Task<bool> DeleteHotelCommentsByHotelIdAsync(Guid hotelId, CancellationToken ct);
}