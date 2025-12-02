using ExerciseService.Domain.Models;

namespace ExerciseService.Application.interfaces.Repositories;

public interface IExerciseRepository
{
    Task<ICollection<Exercise>> GetAllAsync(CancellationToken ct);
    Task<Exercise?> GetByIdAsync(int id, CancellationToken ct);
    Task<bool> DeleteAsync(int id, CancellationToken ct);
    Task<bool> AddAsync(Exercise exercise, CancellationToken ct);
}