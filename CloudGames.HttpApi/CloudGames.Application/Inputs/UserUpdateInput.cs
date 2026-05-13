using CloudGames.Domain.Entities;

namespace CloudGames.Application.Inputs;

public class UserUpdateInput
{
    public required Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Email { get; set; }
    public bool Administrator { get; set; }
    public List<Library> Libraries { get; set; } = new List<Library>();
}
