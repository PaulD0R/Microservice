using HotelService.Application.Interfaces.Repositories;
using HotelService.Application.Interfaces.Services;
using HotelService.Application.Mappers;
using HotelService.Application.Models.RoomStates;
using HotelService.Domain.Exceptions;

namespace HotelService.Application.Services;

public class RoomStateService(
    IRoomStateRepository roomStateRepository) 
    : IRoomStateService
{
    public async Task<IEnumerable<RoomStateDto>> GetRoomStateByHotelRoomIdAsync(Guid roomId, CancellationToken ct)
    {
        var roomStates = await roomStateRepository.GetByRoomIdAsync(roomId, ct);

        return roomStates.Select(s => s.ToRoomStateDto());
    }

    public async Task<bool> AddRoomStateAsync(
        AddRoomStateRequest roomStateRequest,
        Guid roomId,
        Guid hotelId,
        string personId,
        CancellationToken ct)
    {
        var roomState = roomStateRequest.ToRoomState(roomId, hotelId, personId);
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

    public void DeleteRoomStatesByHotelRoomId(Guid roomId, CancellationToken ct)
    {
        roomStateRepository.DeleteByRoomId(roomId, ct);
    }

    public void DeleteRoomStatesByHotelId(Guid hotelId, CancellationToken ct)
    { 
        roomStateRepository.DeleteByHotelId(hotelId, ct);
    }
}