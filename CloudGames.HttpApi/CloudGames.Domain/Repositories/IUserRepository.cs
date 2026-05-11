using CloudGames.Domain.Entities;

namespace CloudGames.Domain.Repositories;

public interface IUserRepository : IRepository<User>
{
    void AddGameByUser(User user, Game game);
}
