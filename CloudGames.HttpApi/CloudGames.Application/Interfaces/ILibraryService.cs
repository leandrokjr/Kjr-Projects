using CloudGames.Application.DTOs;

namespace CloudGames.Application.Interfaces;
public interface ILibraryService
{
    Task<UserResponseDto?> AddGameByUser(Guid userId, Guid gameId);
}

