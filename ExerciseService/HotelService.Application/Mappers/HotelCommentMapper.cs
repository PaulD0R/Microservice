using HotelService.Application.Models.HotelComments;
using HotelService.Domain.Entities;

namespace HotelService.Application.Mappers;

public static class HotelCommentMapper
{
    public static HotelComment ToHotelComment(
        this AddHotelCommentRequest addHotelCommentRequest,
        string personId,
        Guid hotelId)
    {
        return new HotelComment
        {
            HotelId = hotelId,
            PersonId = personId,
            Message = addHotelCommentRequest.Message
        };
    }

    public static HotelCommentDto ToHotelCommentDto(this HotelComment hotelComment, string personName)
    {
        return new HotelCommentDto(
            hotelComment.Id,
            personName,
            hotelComment.Message,
            hotelComment.CreatedAt);
    }
}