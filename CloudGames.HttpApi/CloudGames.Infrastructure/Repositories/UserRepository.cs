using CloudGames.Domain.Entities;
using CloudGames.Domain.Repositories;

namespace CloudGames.Infrastructure.Repositories;

internal class UserRepository : EFRepository<User>, IUserRepository
{
    public UserRepository(ApplicationDbContext context) : base(context)
    {
    }
}
