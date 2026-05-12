using CloudGames.Application.Inputs;
using CloudGames.Domain.Entities;

namespace CloudGames.Application.Interfaces;

public interface IGameService
{
    Task<Game> CreateGame(GameCreateInput input);

    Task<Game?> GetGameById(Guid id);

    Task<List<Game>?> GetAllGames();

    Task<Game?> CreateGamePromotion(Guid id, int percentage);

    Task<Game?> UpdateGame(GameUpdateInput input);

    Task DeleteGame(Guid id);
}
