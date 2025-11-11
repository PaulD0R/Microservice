namespace UserService.Application.Models.Person;

public record PersonDto
{
    public string Id { get; set; } = null!;
    public string Name { get; set; } = null!;
}