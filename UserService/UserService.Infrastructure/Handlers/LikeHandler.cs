using System.Text.Json;
using UserService.Application.Interfaces.Factory;
using UserService.Application.Interfaces.Messages;
using UserService.Domain.Events;

namespace UserService.Infrastructure.Handlers;

public class LikeHandler(ILikeServiceFactory likeServiceFactory) : IMessageHandler<LikeEvent>
{
    public async Task HandleAsync(LikeEvent message, CancellationToken cancellationToken = default)
    {
        var likeEvent = JsonSerializer.Deserialize<LikeEvent>(message.ToString());
        
        var likeService = likeServiceFactory.CreateLikeService();
        await likeService.ChangeLikeAsync(likeEvent);
    }
}