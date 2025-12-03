using ExerciseService.Domain.Models;
using Microsoft.AspNetCore.Http;

namespace ExerciseService.Application.Interfaces.Services;

public interface IFileService
{
    Task<bool> SaveAsync(IFormFile file, CancellationToken ct);
    Task<byte[]> ReadAsync(string fileName, CancellationToken ct);
    Task<bool> DeleteAsync(string fileName, CancellationToken ct);
}