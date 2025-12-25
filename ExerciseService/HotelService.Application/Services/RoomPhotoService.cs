using HotelService.Application.Interfaces.Repositories;
using HotelService.Application.Interfaces.Services;
using HotelService.Application.Mappers;
using HotelService.Application.Models.RoomPhotos;
using HotelService.Domain.Exceptions;

namespace HotelService.Application.Services;

public class RoomPhotoService(
    ICloudRepository cloudRepository,
    IRoomPhotoRepository roomPhotoRepository)
    : IRoomPhotoService
{
    public async Task<IEnumerable<RoomPhotoDto>> GetRoomPhotosByRoomIdAsync(Guid roomId, CancellationToken ct)
    {
        var rooms = await roomPhotoRepository.GetByRoomIdAsync(roomId, ct);
        var result = new List<RoomPhotoDto>();

        foreach (var room in rooms)
        {
            var key = await cloudRepository.GetDownloadUrlAsync(room.Key, ct);
            result.Add(room.ToRoomPhotoDto(key));
        }
        
        return result;
    }

    public async Task<RoomPhotoDto> GetFirstRoomPhotoByRoomIdAsync(Guid roomId, CancellationToken ct)
    {
        var file = await roomPhotoRepository.GetFirstByRoomIdAsync(roomId, ct);

        if (file == null)
            throw new NotFoundException("Photo Not Found");

        var key = await cloudRepository.GetDownloadUrlAsync(file.Key, ct);
        
        return file.ToRoomPhotoDto(key);
    }

    public async Task<string> AddRoomPhotoAsync(AddRoomPhotoRequest hotelPhoto, Guid roomId, CancellationToken ct)
    {
        var key = $"uploads/{DateTime.UtcNow:yyyy/MM}/{Guid.NewGuid()}{Path.GetExtension(hotelPhoto.Name)}";
        var hotelRecord = hotelPhoto.ToRoomPhoto(key);
        
        if (await roomPhotoRepository.AddAsync(hotelRecord, ct))
            throw new BadRequestException("Hotel`s Photo Dont Added");
        
        return await cloudRepository.GetUploadUrlAsync(key, hotelPhoto.Type, ct);
    }
    
    public async Task<bool> DeleteRoomPhotoAsync(Guid id, CancellationToken ct)
    {
        var file = await roomPhotoRepository.GetByIdAsync(id, ct);
        if (file == null) throw new NotFoundException("Photo Not Found");

        await cloudRepository.DeleteAsync(file.Key, ct);
        await roomPhotoRepository.DeleteAsync(id, ct);
        
        return true;
    }

    public async Task<bool> DeleteRoomPhotosByHotelRoomIdAsync(Guid hotelId, CancellationToken ct)
    {
        var photos = await roomPhotoRepository.GetByHotelIdAsync(hotelId, ct);

        foreach (var photo in photos)
        {
            await cloudRepository.DeleteAsync(photo.Key, ct);
        }

        return await roomPhotoRepository.DeleteByHotelIdAsync(hotelId, ct);
    }

    public async Task<bool> DeleteRoomPhotosByHotelIdAsync(Guid hotelId, CancellationToken ct)
    {
        var photos = await roomPhotoRepository.GetByRoomIdAsync(hotelId, ct);

        foreach (var photo in photos)
        {
            await cloudRepository.DeleteAsync(photo.Key, ct);
        }

        return await roomPhotoRepository.DeleteByRoomIdAsync(hotelId, ct);
    }
}