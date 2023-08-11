using PRL.Data.DbContexts;
using PRL.Data.IRepositories;
using PRL.Data.Repositories.Commons;
using PRL.Domain.Entities.Books;
using System.Runtime.CompilerServices;

namespace PRL.Data.Repositories;

public class BookRepository : GenericRepository<Book>,IBookRepository
{
    private readonly AppDbContext appDbContext;
    public BookRepository(AppDbContext appDbContext) : base(appDbContext)
    {
        this.appDbContext = appDbContext;
    }

    public IQueryable<Book> SearchBooks(string title)
    {
        return appDbContext.Books.Where(x => x.Title.Contains(title));
    }
}
