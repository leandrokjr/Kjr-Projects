using CloudGames.Domain.Entities;
using CloudGames.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CloudGames.Infrastructure.Repositories;

internal class UserRepository : EFRepository<User>, IUserRepository
{
    public UserRepository(ApplicationDbContext context) : base(context)
    {
    }

    public void AddGameByUser(User user, Game game)
    {
        user.Library ??= new List<Game>();
        user.Library.Add(game);
    }

    public async Task<User?> GetByEmail(string email)
    {
        return await _context.User.FirstOrDefaultAsync(u => u.Email == email);
    }
}
