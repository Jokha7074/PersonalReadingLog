using PRL.Data.DbContexts;
using PRL.Data.IRepositories;
using PRL.Data.IRepositories.Commons;
using System.Net.WebSockets;

namespace PRL.Data.Repositories.Commons;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext appDbContext;
    public UnitOfWork()
    {
        this.appDbContext = new AppDbContext();
        this.UserRepository = new UserRepository(appDbContext);
        this.BookRepository = new BookRepository(appDbContext);
        this.BookCategoryRepository = new BookCategoryRepository(appDbContext);
        this.SessionRepository = new SessionRepository(appDbContext);
    }
    public IUserRepository UserRepository {get;set;}

    public IBookCategoryRepository BookCategoryRepository { get; set; }

    public IBookRepository BookRepository { get; set; }

    public ISessionRepository SessionRepository { get; set; }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }
    public async Task SaveAsync()
    {
        int a = await appDbContext.SaveChangesAsync();
        Console.WriteLine(a);
    }
}
