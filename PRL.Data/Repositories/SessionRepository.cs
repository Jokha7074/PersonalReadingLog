using PRL.Data.DbContexts;
using PRL.Data.IRepositories;
using PRL.Data.Repositories.Commons;
using PRL.Domain.Entities.Books;

namespace PRL.Data.Repositories;

public class SessionRepository :GenericRepository<Session> ,ISessionRepository
{
    public SessionRepository(AppDbContext appDbContext) : base(appDbContext)
    {
        
    }
}
