namespace HotelService.Application.Models.HotelComments;

public record HotelCommentDto(
    Guid Id,
    string PersonName,
    string Message,
    DateTime CreatedAt);