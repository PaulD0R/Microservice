using UserService.Application.Interfaces.Repositories;
using UserService.Application.Interfaces.Services;
using UserService.Domain.Entities;
using UserService.Domain.Events;
using UserService.Domain.Exceptions;

namespace UserService.Application.Services;

public class LikeService(ILikeRepository likeRepository) : ILikeService
{
    public async Task<bool> ChangeLikeAsync(LikeEvent? likeEvent)
    {
        if (likeEvent == null)
            throw new BadRequestException("idk");

        var userLikes = await likeRepository.GetByUserIdAsync(likeEvent.UserId)
                        ?? new Like { PersonId = likeEvent.UserId };

        switch (likeEvent.Action)
        {
            case LikeAction.Like:
                userLikes.ExerciseIds.Add(likeEvent.PostId);
                break;

            case LikeAction.Unlike:
                userLikes.ExerciseIds.Remove(likeEvent.PostId);
                break;
            
            default: throw new BadRequestException("idk");
        }

        await likeRepository.SaveAsync(userLikes);
        return true;
    }
}