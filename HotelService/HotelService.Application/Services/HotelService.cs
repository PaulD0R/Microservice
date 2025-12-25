using HotelService.Application.Interfaces.Helpers;
using HotelService.Application.Interfaces.Repositories;
using HotelService.Application.Interfaces.Services;
using HotelService.Application.Mappers;
using HotelService.Application.Models.Hotels;
using HotelService.Domain.Exceptions;

namespace HotelService.Application.Services;

public class HotelService(
    IHotelRepository hotelRepository,
    IHotelPhotoService hotelPhotoService,
    IHotelCommentService hotelCommentService,
    IHotelRoomService hotelRoomService,
    IHotelRatingService hotelRatingService,
    IUnitOfWork unitOfWork)
    : IHotelService
{
    public async Task<IEnumerable<HotelDto>> GetHotelsByPageAsync(int pageNumber, int pageSize, CancellationToken ct)
    {
        var hotels = await hotelRepository.GetByPageAsync(pageNumber, pageSize, ct);
        var hotelTasks =  hotels.Select(async h =>
        {
            var photo = await hotelPhotoService.GetFirstHotelPhotoByHotelIdAsync(h.Id, ct);
            
            return h.ToHotelDto(photo);
        });
        
        return await Task.WhenAll(hotelTasks);
    }

    public async Task<HotelFullDto> GetHotelByIdAsync(Guid id, CancellationToken ct)
    {
        var hotel = await hotelRepository.GetByIdAsync(id, ct);
        var photos = await hotelPhotoService.GetHotelPhotosByHotelIdAsync(id, ct);
        
        return hotel != null ? hotel.ToHotelFullDto(photos) : 
            throw new NotFoundException("Hotel Not Found");
    }

    public async Task<bool> AddHotelAsync(AddHotelRequest hotelRequest, CancellationToken ct)
    {
        var hotel = hotelRequest.ToHotel();
        
        return await  hotelRepository.AddAsync(hotel, ct) ? true :
                throw new BadRequestException("Hotel Not Added");
    }

    public async Task<bool> UpdateHotelAsync(Guid id, PatchHotelRequest hotelRequest, CancellationToken ct)
    {
        var hotel = await hotelRepository.GetByIdAsync(id, ct);
        if (hotel == null) throw new NotFoundException("Hotel Not Found");
        
        hotel.ToUpdateHotel(hotelRequest);
        
        return await hotelRepository.UpdateAsync(hotel, ct);
    }

    public async Task<bool> DeleteHotelAsync(Guid id, CancellationToken ct)
    {
        await unitOfWork.BeginTransactionAsync();

        try
        {
            hotelCommentService.DeleteHotelCommentsByHotelId(id, ct);
            
            await hotelPhotoService.DeleteHotelPhotosByHotelIdAsync(id, ct);
            await hotelRatingService.DeleteHotelRatingsByHotelIdAsync(id, ct);
            await hotelRoomService.DeleteHotelRoomsByHotelIdIdAsync(id, ct);
            
            await hotelRepository.DeleteAsync(id, ct);
            
            await unitOfWork.CommitTransactionAsync();
                
            return true;
        }
        catch 
        {
            await unitOfWork.RollbackTransactionAsync();
            throw;
        }
    }
}