using HotelService.Domain.Events;

namespace HotelService.Application.Interfaces.Services;

public interface IPersonService
{
    Task<string> GetNameByIdAsync(string id, CancellationToken ct);
    Task<bool> AddAsync(PersonCreateEvent personEvent, CancellationToken ct);
    Task<bool> UpdateNameAsync(PersonUpdateEvent person, CancellationToken ct);
    Task<bool> DeleteAsync(PersonDeleteEvent person, CancellationToken ct);
}