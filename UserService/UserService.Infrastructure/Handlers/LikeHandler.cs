using System.Text.Json;
using UserService.Application.Interfaces.Messages;
using UserService.Application.Interfaces.Repositories;
using UserService.Domain.Entities;
using UserService.Domain.Events;

namespace UserService.Infrastructure.Handlers;

public class LikeHandler(ILikeRepository likeRepository) : IMessageHandler<LikeEvent>
{
    public async Task HandleAsync(LikeEvent message, CancellationToken cancellationToken = default)
    {
        var likeEvent = JsonSerializer.Deserialize<LikeEvent>(message.ToString());
        if (likeEvent == null)
        {
            return;
        }

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
        }

        await likeRepository.SaveAsync(userLikes);
    }
}