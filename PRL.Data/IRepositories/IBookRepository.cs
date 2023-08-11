using PRL.Data.IRepositories.Commons;
using PRL.Domain.Entities.Books;

namespace PRL.Data.IRepositories;

public interface IBookRepository : IGenericRepository<Book>
{
    IQueryable<Book> SearchBooks(string title);
}
