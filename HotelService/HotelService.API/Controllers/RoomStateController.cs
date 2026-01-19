using HotelService.API.Extensions;
using HotelService.Application.Interfaces.Services;
using HotelService.Application.Models.RoomStates;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotelService.API.Controllers;

[ApiController]
[Authorize]
[Route("hotel-service/hotels")]
public class RoomStateController(
    IRoomStateService roomStateService)
    : ControllerBase
{
    [HttpGet("rooms/{roomId:guid}/states")]
    public async Task<IActionResult> GetStates([FromRoute] Guid roomId, CancellationToken ct)
    {
        return Ok(await roomStateService.GetRoomStateByHotelRoomIdAsync(roomId, ct));
    }

    [HttpPost("{hotelId:guid}/rooms/{roomId:guid}/states")]
    public async Task<IActionResult> AddRoomState(
        [FromRoute] Guid hotelId,
        [FromRoute] Guid roomId, 
        [FromBody] AddRoomStateRequest roomState,
        CancellationToken ct)
    {
        var personId = User.GetId();
        if (personId == null) return Unauthorized();
        if (!ModelState.IsValid) return BadRequest(ModelState);
        
        await roomStateService.AddRoomStateAsync(roomState, roomId, hotelId, personId, ct);
        return Created();
    }

    [HttpDelete("rooms/{roomId:guid}/states")]
    public async Task<IActionResult> DeleteRoomState([FromRoute] Guid roomId, CancellationToken ct)
    {
        await roomStateService.DeleteRoomStateAsync(roomId, ct);
        return NoContent();
    }
}