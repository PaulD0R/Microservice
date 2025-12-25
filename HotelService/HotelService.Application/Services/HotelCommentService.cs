using HotelService.Application.Interfaces.Repositories;
using HotelService.Application.Interfaces.Services;
using HotelService.Application.Mappers;
using HotelService.Application.Models.HotelComments;
using HotelService.Domain.Exceptions;

namespace HotelService.Application.Services;

public class HotelCommentService(
    IHotelCommentRepository hotelCommentRepository,
    IPersonService personService)
    : IHotelCommentService
{
    public async Task<IEnumerable<HotelCommentDto>> GetHotelCommentsByHotelIdAsync(Guid hotelId, CancellationToken ct)
    {
        var comments = await hotelCommentRepository.GetByHotelIdAsync(hotelId, ct);
        var commentsTask = comments.Select(async c =>
        {
            var name = await personService.GetNameByIdAsync(c.PersonId, ct);
            
            return c.ToHotelCommentDto(name);
        });
        
        return await Task.WhenAll(commentsTask);
    }

    public async Task<bool> AddHotelCommentAsync(
        AddHotelCommentRequest hotelComment, 
        string personId, 
        Guid hotelId, 
        CancellationToken ct)
    {
        var comment = hotelComment.ToHotelComment(personId, hotelId);
        
        return await hotelCommentRepository.AddAsync(comment, ct) ?  true : 
                throw new BadRequestException("Comment Not Added");
    }

    public async Task<bool> DeleteHotelCommentAsync(Guid id, CancellationToken ct)
    {
        return await hotelCommentRepository.DeleteAsync(id, ct) ? true :
                throw new NotFoundException("HotelComment Not Found");
    }

    public void DeleteHotelCommentsByHotelId(Guid hotelId, CancellationToken ct)
    {
        hotelCommentRepository.DeleteByHotelIdAsync(hotelId, ct);
    }
}