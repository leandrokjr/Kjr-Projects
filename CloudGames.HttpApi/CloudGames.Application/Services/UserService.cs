using CloudGames.Application.Interfaces;
using CloudGames.Domain.Entities;
using CloudGames.Domain.Inputs;

namespace CloudGames.Application.Services;

public class UserService : IUserService
{
    public async Task<User> CreateUserService(UserCreateInput input)
    {
        string hash = BCrypt.Net.BCrypt.HashPassword(input.Password);

        var user = new User()
        {
            Name = input.Name,
            Email = input.Email,
            Password = hash,
            Administrator = input.Administrator,
            Library = new List<Game>()
        };

        return user;
    }

    public async Task<User?> UpdatePasswordUserService(User user, string currentPassword, string newPassword)
    {
        if (!BCrypt.Net.BCrypt.Verify(currentPassword, user.Password))
            return null;
            
        string hash = BCrypt.Net.BCrypt.HashPassword(newPassword);

        var user1 = new User()
        {
            Name = user.Name,
            Email = user.Email,
            Password = hash,
            Administrator = user.Administrator,
            Library = user.Library
        };

        return user;
    }
}
