using UserService.Application.Models.Token;

namespace UserService.Application.Interfaces.Services;

public interface ITokenService
{
    Task<TokensDto> RefreshTokenAsync(RefreshTokenRequest refreshTokenRequest);   
}