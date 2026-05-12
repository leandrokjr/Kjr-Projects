using CloudGames.Application.Interfaces;
using CloudGames.Domain.Entities;
using CloudGames.Domain.Inputs;
using CloudGames.Domain.Repositories;

namespace CloudGames.Application.Services;

public class GameService : IGameService
{
    private readonly IGameRepository _gameRepository;

    public GameService(IGameRepository gameRepository)
    {
        _gameRepository = gameRepository;
    }

    public async Task<Game> CreateGame(GameCreateInput input)
    {
        var game = new Game()
        {
            Title = input.Title,
            Price = input.Price,
            CurrentPrice = input.Price
        };

        _gameRepository.Create(game);

        return game;
    }

    public async Task<Game?> GetGameById(Guid id)
    {
        var game = _gameRepository.GetById(id);

        if (game == null)
            return null;

        return game;
    }

    public async Task<List<Game>?> GetAllGames()
    {
        var games = _gameRepository.GetAll().ToList();

        if (!games.Any())
            return null;

        return games;
    }

    public async Task<Game?> CreateGamePromotion(Guid id, int percentage)
    {
        var game = _gameRepository.GetById(id);

        if (game == null)
            return game;

        game.ApplyDiscount(percentage);

        _gameRepository.Update(game);

        return game;
    }

    public async Task<Game> UpdateGame(GameUpdateInput input)
    {
        var game = new Game()
        {
            Id = input.Id,
            Title = input.Title,
            Price = input.Price,
            CurrentPrice = input.CurrentPrice
        };

        _gameRepository.Update(game);

        return game;
    }

    public async Task DeleteGame(Guid id)
    {
        _gameRepository.Delete(id);
    }
}
