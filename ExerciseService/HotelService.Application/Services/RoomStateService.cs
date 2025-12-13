using HotelService.Application.Interfaces.Repositories;
using HotelService.Application.Interfaces.Services;
using HotelService.Application.Mappers;
using HotelService.Application.Models.RoomStates;
using HotelService.Domain.Exceptions;

namespace HotelService.Application.Services;

public class RoomStateService(
    IRoomStateRepository roomStateRepository) : IRoomStateService
{
    public async Task<IEnumerable<RoomStateDto>> GetRoomStateByHotelRoomIdAsync(Guid roomId, CancellationToken ct)
    {
        var roomStates = await roomStateRepository.GetByRoomIdAsync(roomId, ct);

        return roomStates.Select(s => s.ToRoomStateDto());
    }

    public async Task<bool> AddRoomStateAsync(AddRoomStateRequest roomStateRequest, Guid roomId, CancellationToken ct)
    {
        var roomState = roomStateRequest.ToRoomState(roomId);
        if (roomState.StartDate >= roomState.EndDate) 
            throw new BadRequestException("Start Date must be before End Date");
        
        var busies = await roomStateRepository.
            GetRoomStatesByDateAsync(roomState.StartDate, roomState.EndDate, ct);
        if (!busies.Any()) 
            throw new BadRequestException("Dates Are Booked");
        
        return await roomStateRepository.AddAsync(roomState, ct) ? true : 
                throw new BadRequestException("Room State Not Added");
    }

    public async Task<bool> DeleteRoomStateAsync(Guid id, CancellationToken ct)
    {
        return await roomStateRepository.DeleteAsync(id, ct) ? true :
            throw new NotFoundException("State Not Found");
    }

    public async Task<bool> DeleteRoomStatesByHotelRoomIdAsync(Guid roomId, CancellationToken ct)
    {
        return await roomStateRepository.DeleteByRoomIdAsync(roomId, ct) ? true :
            throw new NotFoundException("Room Not Found");
    }

    public async Task<bool> DeleteRoomStatesByHotelIdAsync(Guid hotelId, CancellationToken ct)
    {
        return await roomStateRepository.DeleteByHotelIdAsync(hotelId, ct) ? true :
            throw new NotFoundException("Hotel Not Found");
    }
}