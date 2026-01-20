using HotelService.Application.Interfaces.Services;
using HotelService.Application.Models.RoomPhotos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotelService.API.Controllers;

[ApiController]
[Authorize]
[Route("hotels/rooms")]
public class RoomPhotoController(
    IRoomPhotoService roomPhotoService)
    : ControllerBase
{
    [HttpGet("{roomId:guid}/photos")]
    public async Task<IActionResult> GetPhotos([FromRoute] Guid roomId, CancellationToken ct)
    {
        return Ok(await roomPhotoService.GetRoomPhotosByRoomIdAsync(roomId, ct));
    }

    [HttpGet("{roomId:guid}/photos/first")]
    public async Task<IActionResult> GetFirstPhoto([FromRoute] Guid roomId, CancellationToken ct)
    {
        return Ok(await roomPhotoService.GetFirstRoomPhotoByRoomIdAsync(roomId, ct));
    }

    [HttpPost("{roomId:guid}/photos")]
    public async Task<IActionResult> AddPhoto(
        [FromBody] AddRoomPhotoRequest request,
        [FromRoute] Guid hotelId,
        [FromRoute] Guid roomId,
        CancellationToken ct)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        await roomPhotoService.AddRoomPhotoAsync(request, roomId, hotelId, ct);
        return Created();
    }

    [HttpDelete("photos/{photoId:guid}")]
    public async Task<IActionResult> DeletePhoto([FromRoute] Guid photoId, CancellationToken ct)
    {
        await roomPhotoService.DeleteRoomPhotoAsync(photoId, ct);
        return NoContent();
    }
}