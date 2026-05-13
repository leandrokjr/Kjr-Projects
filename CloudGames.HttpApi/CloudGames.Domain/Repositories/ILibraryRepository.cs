using CloudGames.Domain.Entities;

namespace CloudGames.Domain.Repositories;
public interface ILibraryRepository : IRepository<Library>
{
    Task<bool> Exists(Guid userId, Guid gameId);
}

