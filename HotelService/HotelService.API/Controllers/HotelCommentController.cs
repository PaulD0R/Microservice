using HotelService.API.Extensions;
using HotelService.Application.Interfaces.Services;
using HotelService.Application.Models.HotelComments;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotelService.API.Controllers;

[ApiController]
[Authorize]
[Route("hotels")]
public class HotelCommentController(
    IHotelCommentService hotelCommentService)
    : ControllerBase
{
    [HttpGet("{hotelId:guid}/comments")]
    public async Task<IActionResult> GetHotelComments([FromRoute] Guid hotelId, CancellationToken ct)
    {
        return Ok(await hotelCommentService.GetHotelCommentsByHotelIdAsync(hotelId, ct));
    }

    [HttpPost("{hotelId:guid}/comments")]
    public async Task<IActionResult> AddHotelComment(
        [FromBody] AddHotelCommentRequest request,
        [FromRoute] Guid hotelId, 
        CancellationToken ct)
    {
        var personId = User.GetId();
        if (personId == null) return Unauthorized();
        if (!ModelState.IsValid) return BadRequest(ModelState);
        
        await hotelCommentService.AddHotelCommentAsync(request, personId, hotelId, ct);
        return Created();
    }

    [HttpDelete("/comments{commentId:guid}")]
    public async Task<IActionResult> DeleteHotelComment([FromRoute] Guid commentId, CancellationToken ct)
    {
        await hotelCommentService.DeleteHotelCommentAsync(commentId, ct);
        return NoContent();
    }
}