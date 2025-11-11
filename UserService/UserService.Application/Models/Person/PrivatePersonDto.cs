namespace UserService.Application.Models.Person;

public record PrivatePersonDto
{
    public string Id { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
}