using CloudGames.Domain.Entities;
using CloudGames.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CloudGames.Infrastructure.Repositories;

public class EFRepository<T> : IRepository<T> where T : Entity
{
    protected ApplicationDbContext _context;
    protected DbSet<T> _dbSet;


    public EFRepository(ApplicationDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }

    public void Create(T entity)
    {
        _dbSet.Add(entity);
        _context.SaveChanges();
    }

    public void Delete(Guid id)
    {
        _dbSet.Remove(GetById(id));
        _context.SaveChanges();
    }

    public IList<T> GetAll()
    {
        return _dbSet.ToList();
    }

    public T GetById(Guid id)
    {
        return _dbSet.Find(id);
    }

    public void Update(T entity)
    {
        _dbSet.Update(entity);
        _context.SaveChanges();
    }
}
