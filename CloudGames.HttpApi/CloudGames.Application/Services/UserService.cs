using CloudGames.Application.DTOs;
using CloudGames.Application.Inputs;
using CloudGames.Application.Interfaces;
using CloudGames.Domain.Entities;
using CloudGames.Domain.Repositories;

namespace CloudGames.Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IGameRepository _gameRepository;

    public UserService(IUserRepository userRepository, IGameRepository gameRepository)
    {
        _userRepository = userRepository;
        _gameRepository = gameRepository;
    }

    public async Task<UserResponseDto?> ValidateLogin(LoginInput input)
    {
        var id = new Guid("df5ae811-dcba-4bb2-a44f-c502e6fa3efd");

        var hash = BCrypt.Net.BCrypt.HashPassword("7878s4@fgK");

        var user = new User
        {
            Id = id,
            Name = "Player",
            Email = "player@player.com",
            Password = "7878s4@fgK",
            Administrator = true
        };

        if (user == null)
            return null;

        bool isValid = BCrypt.Net.BCrypt.Verify(user.Password, hash);

        if (!isValid)
            return null;

        return new UserResponseDto(user.Id, user.Name, user.Email, user.Administrator, user.Library.ToList());
    }

    public async Task<UserResponseDto> CreateUser(UserCreateInput input)
    {
        string hash = BCrypt.Net.BCrypt.HashPassword(input.Password);

        var user = new User()
        {
            Name = input.Name,
            Email = input.Email,
            Password = hash,
            Administrator = input.Administrator
        };

        _userRepository.Create(user);

        return new UserResponseDto(user.Id, user.Name, user.Email, user.Administrator, user.Library.ToList());
    }

    public async Task<UserResponseDto?> GetUserById(Guid id)
    {
        var user = _userRepository.GetById(id);

        return new UserResponseDto(user.Id, user.Name, user.Email, user.Administrator, user.Library.ToList());
    }

    public async Task<List<UserResponseDto>?> GetAllUsers()
    {
        var users = _userRepository.GetAll().ToList();

        if (!users.Any())
            return null;

        return users.Select(user => new UserResponseDto(user.Id, user.Name, user.Email, user.Administrator, user.Library.ToList())).ToList();
    }

    public async Task<UserResponseDto?> AddGameByUser(Guid userId, Guid gameId)
    {
        var user = _userRepository.GetById(userId);

        if (user == null)
            return null;

        var game = _gameRepository.GetById(gameId);

        _userRepository.AddGameByUser(user, game);

        _userRepository.Update(user);

        return new UserResponseDto(user.Id, user.Name, user.Email, user.Administrator, user.Library.ToList());
    }

    public async Task<UserResponseDto?> UpdatePasswordByUser(UserPasswordUpdateInput input)
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

        return new UserResponseDto(user.Id, user.Name, user.Email, user.Administrator, user.Library.ToList());
    }

    public async Task<UserResponseDto?> UpdateUser(UserUpdateInput input)
    {
        var userById = _userRepository.GetById(input.Id);

        if (userById == null)
            return null;

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

        return new UserResponseDto(user.Id, user.Name, user.Email, user.Administrator, user.Library.ToList());
    }

    public async Task DeleteUser(Guid id)
    {
        _userRepository.Delete(id);
    }
}