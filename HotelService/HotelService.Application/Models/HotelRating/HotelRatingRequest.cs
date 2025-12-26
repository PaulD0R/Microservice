using System.ComponentModel.DataAnnotations;

namespace HotelService.Application.Models.HotelRating;

public record HotelRatingRequest(
    [Required] byte Value);