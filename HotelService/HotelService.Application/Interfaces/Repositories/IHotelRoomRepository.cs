using HotelService.Application.Helpers.FilterModels;
using HotelService.Application.Helpers.SortModels;
using HotelService.Domain.Entities;

namespace HotelService.Application.Interfaces.Repositories;

public interface IHotelRoomRepository
{
    Task<IEnumerable<HotelRoom>> GetAllAsync(CancellationToken ct);
    Task<IEnumerable<HotelRoom>> GetByHotelIdAsync(
        Guid hotelId,
        HotelRoomFilter hotelRoomFilter, 
        HotelRoomSortModel hotelRoomSortModel,
        CancellationToken ct);
    Task<HotelRoom?> GetByIdAsync(Guid hotelRoomId, CancellationToken ct);
    Task<bool> AddAsync(HotelRoom hotelRoom, CancellationToken ct);
    Task<bool> UpdateAsync(HotelRoom hotelRoom, CancellationToken ct);
    Task<bool> DeleteAsync(Guid hotelRoomId, CancellationToken ct);
    void DeleteByHotelId(Guid hotelId, CancellationToken ct);
}