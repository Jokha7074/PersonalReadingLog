using PRL.Domain.Commons;

namespace PRL.Data.IRepositories.Commons;

public interface IGenericRepository<T> where T : Auditable
{
    Task AddAsync(T entity);
    void Modification(T entity);
    void Remove(T entity);
    Task<T> GetByIdAsync(long id);
    IQueryable<T> GetAll();
}
