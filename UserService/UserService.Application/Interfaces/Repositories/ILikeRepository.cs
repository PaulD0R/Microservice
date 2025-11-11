using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;
using UserService.Domain.Entities;

namespace UserService.Application.Interfaces.Repositories;

public interface ILikeRepository
{
    Task<Like?> GetByUserIdAsync(string userId);
    Task SaveAsync(Like userLikes);
}