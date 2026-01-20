using HotelService.Application.Helpers.FilterModels;
using HotelService.Application.Helpers.SortModels;
using HotelService.Application.Interfaces.Services;
using HotelService.Application.Models.HotelRooms;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotelService.API.Controllers;

[ApiController]
[Authorize]
[Route("hotels")]
public class HotelRoomController(
    IHotelRoomService hotelRoomService)
    : ControllerBase
{
    [HttpGet("{hotelId:guid}/rooms")]
    public async Task<IActionResult> GetHotelRooms(
        [FromRoute] Guid hotelId,
        [FromQuery] HotelRoomFilter hotelRoomFilter,
        [FromQuery] HotelRoomSortModel hotelRoomSortModel,
        CancellationToken ct)
    {
        return Ok(await hotelRoomService.
            GetHotelRoomsByHotelIdAsync(hotelId, hotelRoomFilter, hotelRoomSortModel, ct));
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
        if (!ModelState.IsValid) return BadRequest(ModelState);
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