using HotelService.Application.Interfaces.Repositories;
using HotelService.Domain.Entities;
using HotelService.Infrastructure.Core;
using Microsoft.EntityFrameworkCore;

namespace HotelService.Infrastructure.Repositories;

public class PersonRepository(
    ApplicationDbContext context)
    : IPersonRepository
{
    public async Task<Person?> GetByIdAsync(string id, CancellationToken ct)
    {
        return await context.Persons.FirstOrDefaultAsync(p => p.Id == id, ct);
    }

    public async Task<string?> GetNameByIdAsync(string id, CancellationToken ct)
    {
        var person = await context.Persons.FirstOrDefaultAsync(p => p.Id == id, ct);
        return person?.Name;
    }

    public async Task<bool> AddAsync(Person person, CancellationToken ct)
    {
        await context.Persons.AddAsync(person, ct);
        var rows = await context.SaveChangesAsync(ct);
        
        return rows > 0;
    }

    public async Task<bool> UpdateNameAsync(string id, string name, CancellationToken ct)
    {
        var person = await context.Persons.FirstOrDefaultAsync(p => p.Id == id, ct);
        if (person == null) return false;
        
        person.Name = name;
        
        var rows = await context.SaveChangesAsync(ct);
        
        return rows > 0;
    }

    public async Task<bool> DeleteAsync(string id, CancellationToken ct)
    {
        var person = await context.Persons.FirstOrDefaultAsync(p => p.Id == id, ct);
        if (person == null) return false;

        context.Persons.Remove(person);
        var rows = await context.SaveChangesAsync(ct);
        
        return rows > 0;
    }
}