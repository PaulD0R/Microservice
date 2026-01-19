using HotelService.Application.Helpers.FilterModels;
using HotelService.Application.Helpers.SortModels;
using HotelService.Domain.Entities;

namespace HotelService.Application.Interfaces.Repositories;

public interface IHotelRepository
{
    Task<IEnumerable<Hotel>> GetAllAsync(CancellationToken ct);
    Task<IEnumerable<Hotel>> GetByPageAsync(
        int pageNumber, 
        int pageSize,
        HotelFilterModel hotelFilterModel,
        HotelSortModel hotelSortModel,
        CancellationToken ct);
    Task<Hotel?> GetByIdAsync(Guid hotelId, CancellationToken ct);
    Task<bool> UpdateAsync(Hotel hotel, CancellationToken ct);
    Task<bool> AddAsync(Hotel hotel, CancellationToken ct);   
    Task<bool> DeleteAsync(Guid hotelId, CancellationToken ct);
}