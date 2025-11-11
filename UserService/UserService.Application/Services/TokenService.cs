using UserService.Application.Interfaces.Repositories;
using UserService.Application.Interfaces.Services;
using UserService.Application.Models.Token;
using UserService.Domain.Exceptions;

namespace UserService.Application.Services;

public class TokenService(
    IRefreshTokenRepository refreshTokenRepository,
    IJwtRepository jwtRepository) : ITokenService
{
    public async Task<TokensDto> RefreshTokenAsync(RefreshTokenRequest refreshTokenRequest)
    {
        var refreshToken = await refreshTokenRepository.GetRefreshTokenByTokenAsync(refreshTokenRequest.Token!);
        if (refreshToken == null || refreshToken.LiveTime < DateTime.UtcNow)
            throw new BadRequestException("Not correct token");
        
        var jwtToken = await jwtRepository.CreateJwtAsync(refreshToken.Person);
        await refreshTokenRepository.UpdateRefreshToken(refreshToken);
        
        return new TokensDto
        {
            Jwt = jwtToken,
            RefreshToken = refreshToken.Token
        };
    }
}