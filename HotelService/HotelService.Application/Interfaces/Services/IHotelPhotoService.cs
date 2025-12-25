using HotelService.Application.Models.HotelPhotos;

namespace HotelService.Application.Interfaces.Services;

public interface IHotelPhotoService
{
    Task<IEnumerable<HotelPhotoDto>>  GetHotelPhotosByHotelIdAsync(Guid hotelId, CancellationToken ct);
    Task<HotelPhotoDto> GetFirstHotelPhotoByHotelIdAsync(Guid hotelId, CancellationToken ct);
    Task<string> AddHotelPhotoAsync(AddHotelPhotoRequest hotelPhoto, Guid hotelId, CancellationToken ct);
    Task<bool> DeleteHotelPhotoAsync(Guid id, CancellationToken ct);
    Task<bool> DeleteHotelPhotosByHotelIdAsync(Guid hotelId, CancellationToken ct);
}