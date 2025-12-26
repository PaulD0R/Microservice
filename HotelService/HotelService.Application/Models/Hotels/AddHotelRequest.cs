using System.ComponentModel.DataAnnotations;

namespace HotelService.Application.Models.Hotels;

public record AddHotelRequest(
    [Required] string Name,
    [Required] string Country,
    [Required] string City,
    [Required] byte Stars,
    [Required] string Description);