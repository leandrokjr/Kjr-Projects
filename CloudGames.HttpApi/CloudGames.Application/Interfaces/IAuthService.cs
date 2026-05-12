using CloudGames.Application.DTOs;

namespace CloudGames.Application.Interfaces;

public interface IAuthService
{
    string GenerateToken(UserResponseDto user);
}
