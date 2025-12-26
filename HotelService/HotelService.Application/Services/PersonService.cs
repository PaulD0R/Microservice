using HotelService.Application.Interfaces.Repositories;
using HotelService.Application.Interfaces.Services;
using HotelService.Application.Mappers;
using HotelService.Domain.Events;
using HotelService.Domain.Exceptions;

namespace HotelService.Application.Services;

public class PersonService(
    IPersonRepository personRepository) : IPersonService
{
    public async Task<string> GetNameByIdAsync(string id, CancellationToken ct)
    {
        var personName = await personRepository.GetNameByIdAsync(id, ct);
        
        return personName ?? throw new NotFoundException("Person Not Found");
    }

    public async Task<bool> AddAsync(PersonCreateEvent personEvent, CancellationToken ct)
    {
        var person = personEvent.ToPerson();
        return await personRepository.AddAsync(person, ct) ? true :
                throw new BadRequestException("Person Not Added");
    }

    public async Task<bool> UpdateNameAsync(PersonUpdateEvent person, CancellationToken ct)
    {
        return await personRepository.UpdateNameAsync(person.PersonId, person.NewName, ct) ? true :
            throw new NotFoundException("Person Not Found");
    }

    public async Task<bool> DeleteAsync(PersonDeleteEvent person, CancellationToken ct)
    {
        return await personRepository.DeleteAsync(person.PersonId, ct) ? true :
            throw new NotFoundException("Person Not Found");
    }
}