using CloudGames.Domain.Entities;

namespace CloudGames.Application.DTOs;

public class UserResponseDto
{
    public UserResponseDto(Guid id, string name, string email, bool administrator, List<GameDto> library)
    {
        Id = id;
        Name = name;
        Email = email;
        Administrator = administrator;
        Library = library;
    }

    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public bool Administrator { get; set; }
    public List<GameDto> Library { get; set; }
}

public class GameDto 
{
    public GameDto(Guid id, string title)
    {
        Id = id;
        Title = title;
    }

    public Guid Id { get; set; }
    public string Title { get; set; }
}
