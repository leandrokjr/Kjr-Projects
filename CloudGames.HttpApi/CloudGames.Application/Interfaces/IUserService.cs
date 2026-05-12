using CloudGames.Application.Inputs;
using CloudGames.Domain.Entities;

namespace CloudGames.Application.Interfaces;

public interface IUserService
{
    Task<User> CreateUser(UserCreateInput input);

    Task<User?> GetUserById(Guid id);

    Task<List<User>?> GetAllUsers();

    Task<User?> AddGameByUser(Guid userId, Guid gameId);

    Task<User?> UpdatePasswordByUser(UserPasswordUpdateInput input);

    Task<User?> UpdateUser(UserUpdateInput input);

    Task DeleteUser(Guid id);
}
