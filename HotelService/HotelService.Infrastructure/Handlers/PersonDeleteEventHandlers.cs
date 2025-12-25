using HotelService.Application.Interfaces.Factories;
using HotelService.Application.Interfaces.Messages;
using HotelService.Domain.Events;

namespace HotelService.Infrastructure.Handlers;

public class PersonDeleteEventHandlers(
    IPersonServiceFactory personServiceFactory)
    : IMessageHandler<PersonDeleteEvent>
{
    public async Task HandleAsync(PersonDeleteEvent message, CancellationToken ct)
    {
        var personService = personServiceFactory.CreatePersonService();
        await personService.DeleteAsync(message, ct);
    }
}