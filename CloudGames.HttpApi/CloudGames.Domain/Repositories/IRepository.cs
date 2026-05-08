namespace CloudGames.Domain.Repositories;

public interface IRepository<T>
{
    IList<T> GetAll();
    T GetById(Guid id);
    void Create(T entity);
    void Update(T entity);
    void Delete(Guid id);
}