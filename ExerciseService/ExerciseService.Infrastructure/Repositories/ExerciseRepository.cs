using ExerciseService.Application.Interfaces.Repositories;
using ExerciseService.Domain.Models;
using ExerciseService.Infrastructure.Core;
using Microsoft.EntityFrameworkCore;

namespace ExerciseService.Infrastructure.Repositories;

public class ExerciseRepository
    (ApplicationDbContext context)
    : IExerciseRepository
{
    public async Task<ICollection<Exercise>> GetAllAsync(CancellationToken ct)
    {
        return await context.Exercises.ToListAsync(ct);
    }

    public async Task<Exercise?> GetByIdAsync(Guid id, CancellationToken ct)
    {
        return await context.Exercises.FirstOrDefaultAsync(x => x.Id.Equals(id), ct);
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken ct)
    {
        var person = await context.Exercises.FirstOrDefaultAsync(x => x.Id.Equals(id), ct);
        if (person == null) return false;
        
        context.Exercises.Remove(person);
        await context.SaveChangesAsync(ct);

        return true;
    }

    public async Task<bool> AddAsync(Exercise exercise, CancellationToken ct)
    {
        var result = await context.Exercises.AddAsync(exercise, ct);
        await context.SaveChangesAsync(ct);
        
        return result.;
    }
}