using CloudGames.Domain.Entities;

namespace CloudGames.Application.Interfaces;

public interface IGameService
{
    Task<Game> CreateGamePromotion(Game gameById, int percentage);
}
