using HotelService.Application.Helpers.FilterModels;
using HotelService.Application.Helpers.SortModels;
using HotelService.Application.Interfaces.Helpers;
using HotelService.Application.Interfaces.Repositories;
using HotelService.Application.Interfaces.Services;
using HotelService.Application.Mappers;
using HotelService.Application.Models.HotelRooms;
using HotelService.Domain.Exceptions;

namespace HotelService.Application.Services;

public class HotelRoomService(
    IHotelRoomRepository hotelRoomRepository,
    IRoomStateService roomStateService,
    IRoomPhotoService roomPhotoService,
    IUnitOfWork unitOfWork)
    : IHotelRoomService
{
    public async Task<IEnumerable<HotelRoomDto>> GetHotelRoomsByHotelIdAsync(
        Guid hotelId,
        HotelRoomFilter hotelRoomFilter, 
        HotelRoomSortModel hotelRoomSortModel,
        CancellationToken ct)
    {
        var rooms = await hotelRoomRepository
            .GetByHotelIdAsync(hotelId, hotelRoomFilter, hotelRoomSortModel, ct);
        var roomTasks = rooms.Select(async r =>
        {
            var photo = await roomPhotoService.GetFirstRoomPhotoByRoomIdAsync(r.Id, ct);
            return r.ToHotelRoomDto(photo);
        });
        
        return await Task.WhenAll(roomTasks);
    }

    public async Task<HotelRoomFullDto> GetHotelRoomByIdAsync(Guid roomId, CancellationToken ct)
    {
        var room = await hotelRoomRepository.GetByIdAsync(roomId, ct);
        var photos = await roomPhotoService.GetRoomPhotosByRoomIdAsync(roomId, ct);
        
        return room != null ? room.ToHotelRoomFullDto(photos) :
            throw new NotImplementedException("Room Not Found");
    }

    public async Task<bool> AddHotelRoomAsync(AddHotelRoomRequest hotelRoom, Guid hotelId, CancellationToken ct)
    {
        var room = hotelRoom.ToHotelRoom();
        
        return await hotelRoomRepository.AddAsync(room, ct) ? true : 
            throw new BadRequestException("Hotel Not Added");
    }

    public async Task<bool> UpdateHotelRoomAsync(Guid id, PatchHotelRoomRequest hotelRoomRequest, CancellationToken ct)
    {
        var hotelRoom = await hotelRoomRepository.GetByIdAsync(id, ct);
        if (hotelRoom == null) throw new NotFoundException("Room Not Found");
        
        hotelRoom.ToUpdateHotelRoom(hotelRoomRequest);
        
        return await hotelRoomRepository.UpdateAsync(hotelRoom, ct) ? true :
            throw new BadRequestException("Hotel Not Updated");
    }

    public async Task<bool> DeleteHotelRoomAsync(Guid roomId, CancellationToken ct)
    {
        await unitOfWork.BeginTransactionAsync();

        try
        {
            await roomPhotoService.DeleteRoomPhotosByHotelRoomIdAsync(roomId, ct);
            roomStateService.DeleteRoomStatesByHotelRoomId(roomId, ct);
            
            hotelRoomRepository.DeleteByHotelId(roomId, ct);
            
            await unitOfWork.CommitTransactionAsync();
            
            return true;
        }
        catch
        {
            await unitOfWork.RollbackTransactionAsync();
            throw;
        }
    }

    public async Task<bool> DeleteHotelRoomsByHotelIdIdAsync(Guid hotelId, CancellationToken ct)
    {
        roomStateService.DeleteRoomStatesByHotelId(hotelId, ct);
        await roomPhotoService.DeleteRoomPhotosByHotelIdAsync(hotelId, ct);
        
        hotelRoomRepository.DeleteByHotelId(hotelId, ct);
        
        return true;
    }
}