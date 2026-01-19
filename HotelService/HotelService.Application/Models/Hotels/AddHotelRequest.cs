using System.ComponentModel.DataAnnotations;

namespace HotelService.Application.Models.Hotels;

public record AddHotelRequest(
    [Required] string Name,
    [Required] string Country,
    [Required] string City,
    [Required] string FullAddress,
    [Required] double Latitude,
    [Required] double Longitude,
    [Required] byte Stars,
    [Required] string Description);