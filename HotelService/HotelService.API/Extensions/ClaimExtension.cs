using System.Security.Claims;

namespace HotelService.API.Extensions;

public static class ClaimExtension
{
    extension(ClaimsPrincipal claim)
    {
        public string? GetId()
        {
            return claim.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }
    }
}