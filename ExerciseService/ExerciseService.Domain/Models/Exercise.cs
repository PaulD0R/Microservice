using System.ComponentModel.DataAnnotations;

namespace ExerciseService.Domain.Models;

public class Exercise
{
    public  Guid Id { get; set; }
    [MaxLength(50)]public string Name { get; set; } = null!;
    [MaxLength(200)] public string Task { get; set; } = null!;
    public string FilePath { get; set; } = null!;
    public DateTime Created { get; set; } = DateTime.Now;
}