using System.ComponentModel.DataAnnotations;

namespace HotelService.Application.Models.RoomPhotos;

public record AddRoomPhotoRequest(
    [Required] string Name,
    [Required] string Type 
);