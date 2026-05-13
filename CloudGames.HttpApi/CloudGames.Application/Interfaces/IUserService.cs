using CloudGames.Application.DTOs;
using CloudGames.Application.Inputs;

namespace CloudGames.Application.Interfaces;

public interface IUserService
{
    Task<UserResponseDto?> ValidateLogin(LoginInput input);

    Task<UserResponseDto> CreateUser(UserCreateInput input);

    Task<UserResponseDto?> GetUserById(Guid id);

    Task<List<UserResponseDto>?> GetAllUsers();

    Task<UserResponseDto?> UpdatePasswordByUser(UserPasswordUpdateInput input);

    Task<UserResponseDto?> UpdateUser(UserUpdateInput input);

    Task DeleteUser(Guid id);
}
