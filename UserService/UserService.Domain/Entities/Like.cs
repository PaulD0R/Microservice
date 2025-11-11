namespace UserService.Domain.Entities;

public class Like
{
    public Guid Id { get; set; }
    public string PersonId { get; set; } = null!;
    public ICollection<Guid> ExerciseIds { get; set; } = [];
}