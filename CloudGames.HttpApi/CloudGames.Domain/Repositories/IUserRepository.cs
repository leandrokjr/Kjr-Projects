using CloudGames.Domain.Entities;

namespace CloudGames.Domain.Repositories;

public interface IUserRepository : IRepository<User>
{
    Task<User?> GetByEmail(string email);
}
