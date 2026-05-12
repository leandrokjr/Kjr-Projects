using CloudGames.Application.Inputs;
using CloudGames.Application.Interfaces;
using CloudGames.Domain.Entities;
using CloudGames.Domain.Repositories;

namespace CloudGames.Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IGameRepository _gameRepository;

    public UserService(IUserRepository userRepository, IGameRepository gameRepository   )
    {
        _userRepository = userRepository;
        _gameRepository = gameRepository;
    }

    public async Task<User> CreateUser(UserCreateInput input)
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

        _userRepository.Create(user);

        return user;
    }

    public async Task<User?> GetUserById(Guid id)
    {
        var user = _userRepository.GetById(id);

        return user;
    }

    public async Task<List<User>?> GetAllUsers()
    {
        var users = _userRepository.GetAll().ToList();

        if (!users.Any())
            return null;

        return users;
    }

    public async Task<User?> AddGameByUser(Guid userId, Guid gameId)
    {
        var user = _userRepository.GetById(userId);

        if (user == null)
            return user;

        var game = _gameRepository.GetById(gameId);

        _userRepository.AddGameByUser(user, game);

        _userRepository.Update(user);

        return user;
    }

    public async Task<User?> UpdatePasswordByUser(UserPasswordUpdateInput input)
    {
        var user = _userRepository.GetById(input.Id);

        if (user == null || !BCrypt.Net.BCrypt.Verify(input.CurrentPassword, user.Password))
            return null;
            
        string hashNewPassword = BCrypt.Net.BCrypt.HashPassword(input.NewPassword);

        var updatedUser = new User()
        {
            Name = user.Name,
            Email = user.Email,
            Password = hashNewPassword,
            Administrator = user.Administrator,
            Library = user.Library
        };

        _userRepository.Update(updatedUser);

        return user;
    }

    public async Task<User?> UpdateUser(UserUpdateInput input)
    {
        var userById = _userRepository.GetById(input.Id);

        if (userById == null)
            return userById;

        var hashPassword = _userRepository.GetById(input.Id).Password;

        var user = new User()
        {
            Id = userById.Id,
            Name = input.Name,
            Password = hashPassword,
            Email = input.Email,
            Administrator = input.Administrator,
            Library = input.Library
        };

        _userRepository.Update(user);

        return user;
    }

    public async Task DeleteUser(Guid id)
    {
        _userRepository.Delete(id);
    }
}