namespace CloudGames.Domain.Inputs;

public class UserUpdateInput
{
    public required Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
    public bool Administrator { get; set; }
}
