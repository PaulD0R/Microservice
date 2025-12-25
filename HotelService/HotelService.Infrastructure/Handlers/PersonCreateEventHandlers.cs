using HotelService.Application.Interfaces.Factories;
using HotelService.Application.Interfaces.Messages;
using HotelService.Application.Interfaces.Services;
using HotelService.Application.Mappers;
using HotelService.Domain.Events;

namespace HotelService.Infrastructure.Handlers;

public class PersonCreateEventHandlers(
    IPersonServiceFactory personServiceFactory)
    : IMessageHandler<PersonCreateEvent>
{
    public async Task HandleAsync(PersonCreateEvent message, CancellationToken ct)
    {
        var personService = personServiceFactory.CreatePersonService();
        await personService.AddAsync(message, ct);
    }
}