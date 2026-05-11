using CloudGames.Domain.Entities;
using CloudGames.Domain.Inputs;

namespace CloudGames.Application.Interfaces;

public interface IUserService
{
    Task<User> CreateUserService(UserCreateInput input);

    Task<User?> UpdatePasswordUserService(User user, string currentPassword, string newPassword);
}
