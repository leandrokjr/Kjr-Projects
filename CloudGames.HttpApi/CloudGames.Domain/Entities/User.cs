namespace CloudGames.Domain.Entities;

public class User : Entity
{
    public required string Name { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
    public required bool Administrator { get; set; }
    public ICollection<Game> Library { get; set; } = new List<Game>();
}
