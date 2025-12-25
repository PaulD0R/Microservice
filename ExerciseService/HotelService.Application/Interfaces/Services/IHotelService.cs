using HotelService.Application.Models;
using HotelService.Application.Models.Hotels;

namespace HotelService.Application.Interfaces.Services;

public interface IHotelService
{
    Task<IEnumerable<HotelDto>> GetHotelsByPageAsync(int  pageNumber, int pageSize, CancellationToken ct);
    Task<HotelFullDto> GetHotelByIdAsync(Guid id, CancellationToken ct);
    Task<bool> AddHotelAsync(AddHotelRequest hotelRequest, CancellationToken ct);
    Task<bool> UpdateHotelAsync(Guid id, PatchHotelRequest hotelRequest, CancellationToken ct);
    Task<bool> DeleteHotelAsync(Guid id, CancellationToken ct);
}