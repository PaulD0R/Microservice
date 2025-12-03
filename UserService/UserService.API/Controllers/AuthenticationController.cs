using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserService.API.Extensions;
using UserService.Application.Interfaces.Services;
using UserService.Application.Models.Person;
using UserService.Application.Models.Token;

namespace UserService.API.Controllers;

[ApiController]
[Route("user-service/authentication")]
public class AuthenticationController(
    IAuthenticationService authenticationService,
    ITokenService tokenService)
    : ControllerBase
{
    [HttpPost("signin")]
    public async Task<IActionResult> Signin([FromBody] SigninRequest signinRequest)
    {
        return Ok(await authenticationService.SigninAsync(signinRequest));
    }

    [HttpPost("signup")]
    public async Task<IActionResult> Signup([FromBody] SignupRequest newPersonRequest)
    {
        return Ok(await authenticationService.SignupAsync(newPersonRequest));
    }

    [HttpPost("refreshToken")]
    public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequest request)
    {
        return Ok(await tokenService.RefreshTokenAsync(request));
    }

    [HttpDelete("logout")]
    [Authorize]
    public async Task<IActionResult> Logout()
    {
        var personId = User.GetId();
        if (personId == null) return Unauthorized("Не авторизирован");

        await authenticationService.LogoutAsync(personId);
        return NoContent();
    }
}