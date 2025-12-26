using System.ComponentModel.DataAnnotations;

namespace HotelService.Application.Models.HotelRooms;

public record AddHotelRoomRequest(
    [Required] Guid HotelId,
    [Required] byte NumberOfResidents,
    [Required] string Description);