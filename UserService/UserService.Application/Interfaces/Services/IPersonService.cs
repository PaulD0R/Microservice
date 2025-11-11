using UserService.Application.Models.Person;

namespace UserService.Application.Interfaces.Services;

public interface IPersonService
{
    Task<PersonDto> GetByIdAsync(string id);  
    Task<PersonDto> GetByNameAsync(string name);
    Task<PrivatePersonDto> GetMeAsync(string id);
}