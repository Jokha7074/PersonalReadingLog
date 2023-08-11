using Microsoft.EntityFrameworkCore;
using PRL.Data.DbContexts;
using PRL.Data.IRepositories.Commons;
using PRL.Domain.Commons;

namespace PRL.Data.Repositories.Commons;

public class GenericRepository<T> : IGenericRepository<T> where T : Auditable
{
    private AppDbContext appDbContext;
    private DbSet<T> dbSet;

    public GenericRepository(AppDbContext appDbContext)
    {
        this.appDbContext = appDbContext;
        this.dbSet = appDbContext.Set<T>();
    }

    public async Task AddAsync(T entity)
    {
         await dbSet.AddAsync(entity);
    }
    public void Modification(T entity)
    {
        appDbContext.Entry(entity).State = EntityState.Modified;
    }

    public void Remove(T entity)
    {
        dbSet.Remove(entity);
    }

    public async Task<T> GetByIdAsync(long id)
        => await dbSet.FindAsync(id);

    public IQueryable<T> GetAll()
        => dbSet.AsQueryable();
}
