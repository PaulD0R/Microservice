using HotelService.Application.Interfaces.Services;
using HotelService.Application.Models.HotelRooms;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotelService.API.Controllers;

[ApiController]
[Authorize]
[Route("hotel-service/hotels")]
public class HotelRoomController(
    IHotelRoomService hotelRoomService)
    : ControllerBase
{
    [HttpGet("{hotelId:guid}/rooms")]
    public async Task<IActionResult> GetHotelRooms([FromRoute] Guid hotelId, CancellationToken ct)
    {
        return Ok(await hotelRoomService.GetHotelRoomsByHotelIdAsync(hotelId, ct));
    }

    [HttpGet("rooms/{roomId:guid}")]
    public async Task<IActionResult> GetHotelRoom([FromRoute] Guid roomId, CancellationToken ct)
    {
        return Ok(await hotelRoomService.GetHotelRoomByIdAsync(roomId, ct));
    }

    [HttpPost("{hotelId:guid}/rooms")]
    public async Task<IActionResult> AddHotelRoom(
        [FromRoute] Guid hotelId,
        [FromBody] AddHotelRoomRequest request,
        CancellationToken ct)
    {
        await hotelRoomService.AddHotelRoomAsync(request, hotelId, ct);
        return Created();
    }
    
    [HttpPut("rooms/{roomId:guid}")]
    public async Task<IActionResult> UpdateHotelRoom(
        [FromRoute] Guid roomId,
        [FromBody] PatchHotelRoomRequest request,
        CancellationToken ct) 
    {
        await hotelRoomService.UpdateHotelRoomAsync(roomId, request, ct);
        return NoContent();
    }

    [HttpDelete("rooms/{roomId:guid}")]
    public async Task<IActionResult> DeleteHotelRoom([FromRoute] Guid roomId, CancellationToken ct)
    {
        await hotelRoomService.DeleteHotelRoomAsync(roomId, ct);
        return NoContent();
    }
}