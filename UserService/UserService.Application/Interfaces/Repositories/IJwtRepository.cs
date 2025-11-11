using UserService.Domain.Entities;

namespace UserService.Application.Interfaces.Repositories;

public interface IJwtRepository
{
    Task<string> CreateJwtAsync(Person person);
}