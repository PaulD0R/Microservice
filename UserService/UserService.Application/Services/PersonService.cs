using UserService.Application.Interfaces.Cachings;
using UserService.Application.Interfaces.Repositories;
using UserService.Application.Interfaces.Services;
using UserService.Application.Mappers;
using UserService.Application.Models.Person;
using UserService.Domain.Exceptions;

namespace UserService.Application.Services;

public class PersonService(
    IPersonRepository personRepository,
    IHashCachingService  cachingService)
    : IPersonService
{
    public async Task<PersonDto> GetByIdAsync(string id)
    {
        var key = $"user:{id}";
        var personDto = await cachingService.GetAsync<PersonDto>(key);
        if (personDto != null) return personDto;
            
        var person = await personRepository.GetByIdAsync(id);
        if (person == null) throw new NotFoundException($"Person {id} not found");;
                
        await cachingService.SetAsync(key, person.ToPrivatePersonDto());
        
        return person.ToPersonDto();
    }

    public async Task<PersonDto> GetByNameAsync(string name)
    {
        var person = await personRepository.GetByNameAsync(name);
        return person?.ToPersonDto() ??
               throw new NotFoundException($"Person {name} not found");
    }

    public async Task<PrivatePersonDto> GetMeAsync(string id)
    {
        var key = $"user:{id}";
        var personDto = await cachingService.GetAsync<PrivatePersonDto>(key);
        if (personDto != null) return personDto;
            
        var person = await personRepository.GetByIdAsync(id);
        if (person == null) throw new NotFoundException($"Person {id} not found");;
                
        await cachingService.SetAsync(key, person.ToPrivatePersonDto());
                
        return person.ToPrivatePersonDto();
    }
}