using UserService.Domain.Events;

namespace UserService.Application.Interfaces.Services;

public interface ILikeService
{
    Task<bool> ChangeLikeAsync(LikeEvent? likeEvent);
}