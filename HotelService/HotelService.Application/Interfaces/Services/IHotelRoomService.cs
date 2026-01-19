using HotelService.Application.Helpers.FilterModels;
using HotelService.Application.Helpers.SortModels;
using HotelService.Application.Models.HotelRooms;

namespace HotelService.Application.Interfaces.Services;

public interface IHotelRoomService
{
    Task<IEnumerable<HotelRoomDto>> GetHotelRoomsByHotelIdAsync(
        Guid hotelId, 
        HotelRoomFilter hotelRoomFilter, 
        HotelRoomSortModel hotelRoomSortModel,
        CancellationToken ct);
    Task<HotelRoomFullDto> GetHotelRoomByIdAsync(Guid roomId, CancellationToken ct);
    Task<bool> AddHotelRoomAsync(AddHotelRoomRequest hotelRoom, Guid hotelId, CancellationToken ct);
    Task<bool> UpdateHotelRoomAsync(Guid id, PatchHotelRoomRequest hotelRoomRequest, CancellationToken ct);
    Task<bool> DeleteHotelRoomAsync(Guid roomId, CancellationToken ct);
    Task<bool> DeleteHotelRoomsByHotelIdIdAsync(Guid hotelId, CancellationToken ct);
}