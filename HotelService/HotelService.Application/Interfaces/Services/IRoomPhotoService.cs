using HotelService.Application.Models.RoomPhotos;

namespace HotelService.Application.Interfaces.Services;

public interface IRoomPhotoService
{
    Task<IEnumerable<RoomPhotoDto>> GetRoomPhotosByRoomIdAsync(Guid roomId, CancellationToken ct);
    Task<RoomPhotoDto> GetFirstRoomPhotoByRoomIdAsync(Guid roomId, CancellationToken ct);
    Task<string> AddRoomPhotoAsync(
        AddRoomPhotoRequest hotelPhoto,
        Guid roomId,
        Guid hotelId,
        CancellationToken ct);
    Task<bool> DeleteRoomPhotoAsync(Guid id, CancellationToken ct);
    Task<bool> DeleteRoomPhotosByHotelRoomIdAsync(Guid hotelId, CancellationToken ct);
    Task<bool> DeleteRoomPhotosByHotelIdAsync(Guid hotelId, CancellationToken ct);
}