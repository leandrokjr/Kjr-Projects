using CloudGames.Application.DTOs;
using CloudGames.Application.Inputs;

namespace CloudGames.Application.Interfaces;

public interface IGameService
{
    Task<GameResponseDto> CreateGame(GameCreateInput input);

    Task<GameResponseDto?> GetGameById(Guid id);

    Task<List<GameResponseDto>?> GetAllGames();

    Task<GameResponseDto?> CreateGamePromotion(Guid id, int percentage);

    Task<GameResponseDto?> UpdateGame(GameUpdateInput input);

    Task DeleteGame(Guid id);
}
