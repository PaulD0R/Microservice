using System.ComponentModel.DataAnnotations;

namespace HotelService.Application.Models.RoomStates;

public record AddRoomStateRequest(
    [Required] DateOnly StartDate,
    [Required] DateOnly EndDate);