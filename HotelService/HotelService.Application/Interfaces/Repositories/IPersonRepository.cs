using HotelService.Domain.Entities;

namespace HotelService.Application.Interfaces.Repositories;
  
public interface IPersonRepository
{
    Task<Person?> GetByIdAsync(string id, CancellationToken ct);
    Task<string?> GetNameByIdAsync(string id, CancellationToken ct);
    Task<bool> AddAsync(Person person, CancellationToken ct);
    Task<bool> UpdateNameAsync(string name, CancellationToken ct);
    Task<bool> DeleteAsync(string id, CancellationToken ct);
}