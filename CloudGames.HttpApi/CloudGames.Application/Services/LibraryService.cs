using CloudGames.Application.DTOs;
using CloudGames.Application.Interfaces;
using CloudGames.Domain.Entities;
using CloudGames.Domain.Repositories;

namespace CloudGames.Application.Services;
public class LibraryService : ILibraryService
{
    private readonly ILibraryRepository _libraryRepository;
    private readonly IUserRepository _userRepository;
    private readonly IGameRepository _gameRepository;

    public LibraryService(ILibraryRepository libraryRepository, IUserRepository userRepository, IGameRepository gameRepository)
    {
        _libraryRepository = libraryRepository;
        _userRepository = userRepository;
        _gameRepository = gameRepository;
    }

    public async Task<UserResponseDto?> AddGameByUser(Guid userId, Guid gameId)
    {
        var user = _userRepository.GetById(userId);

        var game = _gameRepository.GetById(gameId);

        if (user == null || game == null)
            return null;

        var userLibrary = new Library
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            User = user,
            GameId = gameId,
            Game = game
        };

        if (await _libraryRepository.Exists(userId, gameId))
            return null;

        var libraryRegister = new Library
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            GameId = gameId
        };

        _libraryRepository.Create(libraryRegister);

        var updatedUser = _userRepository.GetById(userId);

        return new UserResponseDto(updatedUser.Id, updatedUser.Name, updatedUser.Email, updatedUser.Administrator, updatedUser.Libraries.ToList());
    }
}

