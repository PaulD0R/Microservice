using UserService.Domain.Entities;

namespace UserService.Application.Interfaces.Repositories;

public interface IRefreshTokenRepository
{
    Task<string> CreateNewRefreshTokenAsync(Person person);
    Task<bool> DeleteOldRefreshTokensAsync(string personId);
    Task<RefreshToken?> GetRefreshTokenByTokenAsync(string token);
    Task<RefreshToken> UpdateRefreshToken(RefreshToken refreshToken);
    Task<bool> DeleteRefreshToken(string id);
}