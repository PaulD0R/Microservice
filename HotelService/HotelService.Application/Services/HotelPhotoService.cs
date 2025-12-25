using HotelService.Application.Interfaces.Repositories;
using HotelService.Application.Interfaces.Services;
using HotelService.Application.Mappers;
using HotelService.Application.Models.HotelPhotos;
using HotelService.Domain.Exceptions;

namespace HotelService.Application.Services;

public class HotelPhotoService(
    ICloudRepository cloudRepository,
    IHotelPhotoRepository hotelPhotoRepository)
    : IHotelPhotoService
{
    private static readonly HotelPhotoDto DefaultHotelPhotoDto = new(
        Name: "no-image.png",
        Key: "https://hotel-service-bucket.storage.yandexcloud.net/static/no-photo.png");
    
    public async Task<IEnumerable<HotelPhotoDto>> GetHotelPhotosByHotelIdAsync(Guid hotelId, CancellationToken ct)
    {
        var files = await hotelPhotoRepository.GetByHotelIdAsync(hotelId, ct);
        var result = new List<HotelPhotoDto>();

        foreach (var file in files)
        {
            var key = await cloudRepository.GetDownloadUrlAsync(file.Key, ct);
            result.Add(file.ToHotelPhotoDto(key));
        }
        
        return result;
    }

    public async Task<HotelPhotoDto> GetFirstHotelPhotoByHotelIdAsync(Guid hotelId, CancellationToken ct)
    {
        var file = await hotelPhotoRepository.GetFirstByHotelIdAsync(hotelId, ct);

        if (file == null)
            return DefaultHotelPhotoDto;

        var key = await cloudRepository.GetDownloadUrlAsync(file.Key, ct);
        
        return file.ToHotelPhotoDto(key);
    }

    public async Task<string> AddHotelPhotoAsync(AddHotelPhotoRequest hotelPhoto, Guid hotelId, CancellationToken ct)
    {
        var key = $"uploads/{DateTime.UtcNow:yyyy/MM}/{Guid.NewGuid()}{Path.GetExtension(hotelPhoto.Name)}";
        var hotelRecord = hotelPhoto.ToHotelPhoto(key, hotelId);
        
        if (await hotelPhotoRepository.AddAsync(hotelRecord, ct))
            throw new BadRequestException("Hotel`s Photo Dont Added");
        
        return await cloudRepository.GetUploadUrlAsync(key, hotelPhoto.Type, ct);
    }

    public async Task<bool> DeleteHotelPhotoAsync(Guid id, CancellationToken ct)
    {
        var file = await hotelPhotoRepository.GetByIdAsync(id, ct);
        if (file == null) throw new NotFoundException("Photo Not Found");

        await cloudRepository.DeleteAsync(file.Key, ct);
        await hotelPhotoRepository.DeleteAsync(id, ct);
        
        return true;
    }

    public async Task<bool> DeleteHotelPhotosByHotelIdAsync(Guid hotelId, CancellationToken ct)
    {
        var photos = await hotelPhotoRepository.GetByHotelIdAsync(hotelId, ct);

        foreach (var photo in photos)
        {
            await cloudRepository.DeleteAsync(photo.Key, ct);
        }

        hotelPhotoRepository.DeleteByHotelId(hotelId, ct);

        return true;
    }
}