using CloudGames.Domain.Entities;
using CloudGames.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CloudGames.Infrastructure.Repositories;
public class LibraryRepository : EFRepository<Library>, ILibraryRepository
{
    public LibraryRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<bool> Exists(Guid userId, Guid gameId)
    {
        return await _context.Set<Library>().AnyAsync(l => l.UserId == userId && l.GameId == gameId);
    }
}
