using CloudGames.Domain.Entities;

namespace CloudGames.Application.Services;

public class GameService
{
    public async Task<Game> CreateGamePromotion(Game gameById, int percentage)
    {
        var pricePromotion = gameById.Price - (gameById.Price * percentage / 100);

        var game = new Game()
        {
            Id = gameById.Id,
            Title = gameById.Title,
            Price = gameById.Price,
            CurrentPrice = pricePromotion
        };

        return game;
    }
}
