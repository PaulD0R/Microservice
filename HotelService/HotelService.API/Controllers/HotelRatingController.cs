using HotelService.API.Extensions;
using HotelService.Application.Interfaces.Services;
using HotelService.Application.Models.HotelRating;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotelService.API.Controllers;

[ApiController]
[Authorize]
[Route("hotel-service/hotels/{hotelId:guid}ratings")]
public class HotelRatingController(
    IHotelRatingService hotelRatingService)
    : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetHotelRatings([FromRoute] Guid hotelId, CancellationToken ct)
    {
        return Ok(await hotelRatingService.GetHotelRatingByHotelIdAsync(hotelId, ct));
    }

    [HttpPut]
    public async Task<IActionResult> ChangeRating(
        [FromRoute] Guid hotelId,
        [FromBody] HotelRatingRequest request,
        CancellationToken ct)
    {
        var personId = User.GetId();
        if (personId == null) return Unauthorized();

        await hotelRatingService.ChangeHotelRatingAsync(request, personId, hotelId, ct);
        return NoContent();
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteHotelRatingAsync([FromRoute] Guid hotelId, CancellationToken ct)
    {
        var personId = User.GetId();
        if (personId == null) return Unauthorized();

        await hotelRatingService.DeleteHotelRatingAsync(hotelId, personId, ct);
        return NoContent();
    }
}