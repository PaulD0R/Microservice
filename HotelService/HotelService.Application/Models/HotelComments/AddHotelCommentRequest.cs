using System.ComponentModel.DataAnnotations;

namespace HotelService.Application.Models.HotelComments;

public record AddHotelCommentRequest(
    [Required] string Message);