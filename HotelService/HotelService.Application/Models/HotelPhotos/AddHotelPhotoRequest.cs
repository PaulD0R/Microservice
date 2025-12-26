using System.ComponentModel.DataAnnotations;

namespace HotelService.Application.Models.HotelPhotos;

public record AddHotelPhotoRequest(
    [Required] string Name,
    string Type
);