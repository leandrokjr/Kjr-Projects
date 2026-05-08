using CloudGames.Domain.Entities;
using CloudGames.Domain.Repositories;

namespace CloudGames.Infrastructure.Repositories;

public class GameRepository : EFRepository<Game>, IGameRepository
{
    public GameRepository(ApplicationDbContext context) : base(context)
    {
    }
}
