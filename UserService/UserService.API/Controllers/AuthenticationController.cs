using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserService.API.Extensions;
using UserService.Application.Interfaces.Services;
using UserService.Application.Models.Person;
using UserService.Application.Models.Token;

namespace UserService.API.Controllers;

[ApiController]
[Route("WPF/Authentication")]
public class AuthenticationController(
    IAuthenticationService authenticationService,
    ITokenService tokenService)
    : ControllerBase
{
    [HttpPost("Signin")]
    public async Task<IActionResult> Signin([FromBody] SigninRequest signinRequest)
    {
        return Ok(await authenticationService.SigninAsync(signinRequest));
    }

    [HttpPost("Signup")]
    public async Task<IActionResult> Signup([FromBody] SignupRequest newPersonRequest)
    {
        return Ok(await authenticationService.SignupAsync(newPersonRequest));
    }

    [HttpPost("RefreshToken")]
    public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequest request)
    {
        return Ok(await tokenService.RefreshTokenAsync(request));
    }

    [HttpDelete("Logout")]
    [Authorize]
    public async Task<IActionResult> Logout()
    {
        var personId = User.GetId();
        if (personId == null) return Unauthorized("Не авторизирован");

        await authenticationService.LogoutAsync(personId);
        return NoContent();
    }
}