using HotelService.Application.Interfaces.Factories;
using HotelService.Application.Interfaces.Messages;
using HotelService.Domain.Events;

namespace HotelService.Infrastructure.Handlers;

public class PersonUpdateEventHandlers(
    IPersonServiceFactory personServiceFactory)
    : IMessageHandler<PersonUpdateEvent>
{
    public async Task HandleAsync(PersonUpdateEvent message, CancellationToken ct)
    {
        var personService = personServiceFactory.CreatePersonService();
        await personService.UpdateNameAsync(message, ct);
    }
}