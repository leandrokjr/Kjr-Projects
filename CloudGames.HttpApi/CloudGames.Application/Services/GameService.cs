using CloudGames.Application.DTOs;
using CloudGames.Application.Inputs;
using CloudGames.Application.Interfaces;
using CloudGames.Domain.Entities;
using CloudGames.Domain.Repositories;

namespace CloudGames.Application.Services;

public class GameService : IGameService
{
    private readonly IGameRepository _gameRepository;

    public GameService(IGameRepository gameRepository)
    {
        _gameRepository = gameRepository;
    }

    public async Task<GameResponseDto> CreateGame(GameCreateInput input)
    {
        var game = new Game()
        {
            Title = input.Title,
            Price = input.Price,
            CurrentPrice = input.Price
        };

        _gameRepository.Create(game);

        return new GameResponseDto(game.Id, game.Title, game.Price, game.CurrentPrice);
    }

    public async Task<GameResponseDto?> GetGameById(Guid id)
    {
        var game = _gameRepository.GetById(id);

        if (game == null)
            return null;

        return new GameResponseDto(game.Id, game.Title, game.Price, game.CurrentPrice);
    }

    public async Task<List<GameResponseDto>?> GetAllGames()
    {
        var games = _gameRepository.GetAll().ToList();

        if (!games.Any())
            return null;

        return games.Select(game => new GameResponseDto(game.Id, game.Title, game.Price, game.CurrentPrice)).ToList();
    }

    public async Task<GameResponseDto?> CreateGamePromotion(Guid id, int percentage)
    {
        var game = _gameRepository.GetById(id);

        if (game == null)
            return null;

        game.ApplyDiscount(percentage);

        _gameRepository.Update(game);

        return new GameResponseDto(game.Id, game.Title, game.Price, game.CurrentPrice);
    }

    public async Task<GameResponseDto?> UpdateGame(GameUpdateInput input)
    {
        var gameById = _gameRepository.GetById(input.Id);

        if (gameById == null)
            return null;

        var game = new Game()
        {
            Id = gameById.Id,
            Title = input.Title,
            Price = input.Price,
            CurrentPrice = input.CurrentPrice
        };

        _gameRepository.Update(game);

        return new GameResponseDto(game.Id, game.Title, game.Price, game.CurrentPrice);
    }

    public async Task DeleteGame(Guid id)
    {
        _gameRepository.Delete(id);
    }
}
