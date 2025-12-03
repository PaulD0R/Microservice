using ExerciseService.Application.Models;
using ExerciseService.Domain.Models;

namespace ExerciseService.Application.interfaces.Services;

public interface IExerciseService
{
    Task<ICollection<Exercise>> GetAllAsync(CancellationToken ct);
    Task<Exercise?> GetByIdAsync(int id, CancellationToken ct);
    Task<bool> DeleteAsync(int id, CancellationToken ct);
}