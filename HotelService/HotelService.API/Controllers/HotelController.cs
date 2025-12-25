using HotelService.Application.Interfaces.Services;
using HotelService.Application.Models.Hotels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotelService.API.Controllers;

[ApiController]
[Authorize]
[Route("hotel-service/hotels")]
public class HotelController(
    IHotelService hotelService)
    : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult> GetHotelComments(
        CancellationToken ct,
        [FromQuery] int pageNumber = 1, 
        [FromQuery] int pageSize = 20)
    {
        return Ok(await hotelService.GetHotelsByPageAsync(pageNumber, pageSize, ct));
    }

    [HttpGet("{hotelId:guid}")]
    public async Task<IActionResult> GetHotelById([FromRoute] Guid hotelId, CancellationToken ct)
    {
        return Ok(await hotelService.GetHotelByIdAsync(hotelId, ct));
    }

    [HttpPost]
    public async Task<IActionResult> AddHotel([FromBody] AddHotelRequest request, CancellationToken ct)
    {
        await hotelService.AddHotelAsync(request, ct);
        return Created();
    }

    [HttpPatch("{hotelId:guid}")]
    public async Task<IActionResult> PatchHotel(
        [FromRoute] Guid hotelId,
        [FromBody] PatchHotelRequest request, 
        CancellationToken ct)
    {
        await hotelService.UpdateHotelAsync(hotelId, request, ct);
        return NoContent();
    }

    [HttpDelete("{hotelId:guid}")]
    public async Task<IActionResult> DeleteHotel([FromRoute] Guid hotelId, CancellationToken ct)
    {
        await hotelService.DeleteHotelAsync(hotelId, ct);
        return NoContent();
    }
}