using HotelService.Application.Interfaces.Services;
using HotelService.Application.Models.HotelPhotos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotelService.API.Controllers;

[ApiController]
[Authorize]
[Route("hotels")]
public class HotelPhotoController(
    IHotelPhotoService hotelPhotoService)
    : ControllerBase
{
    [HttpGet("{hotelId:guid}/photos")]
    public async Task<IActionResult> GetHotelPhotos([FromRoute] Guid hotelId, CancellationToken ct)
    {
       return Ok(await hotelPhotoService.GetHotelPhotosByHotelIdAsync(hotelId, ct));
    }

    [HttpGet("{hotelId:guid}/photos/first")]
    public async Task<IActionResult> GetFirstPhoto([FromRoute] Guid hotelId, CancellationToken ct)
    {
        return Ok(await hotelPhotoService.GetFirstHotelPhotoByHotelIdAsync(hotelId, ct));
    }

    [HttpPost("{hotelId:guid}/photos")]
    public async Task<IActionResult> AddPhoto(
        [FromBody] AddHotelPhotoRequest request,
        [FromRoute] Guid hotelId,
        CancellationToken ct)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        await hotelPhotoService.AddHotelPhotoAsync(request, hotelId, ct);
        return Created();
    }

    [HttpDelete("photos{photoId:guid}")]
    public async Task<IActionResult> DeletePhoto([FromRoute] Guid photoId, CancellationToken ct)
    {
        await hotelPhotoService.DeleteHotelPhotoAsync(photoId, ct);
        return NoContent();
    }
}