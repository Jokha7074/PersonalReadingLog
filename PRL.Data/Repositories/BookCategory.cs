using PRL.Data.DbContexts;
using PRL.Data.IRepositories;
using PRL.Data.Repositories.Commons;
using PRL.Domain.Entities.Books;

namespace PRL.Data.Repositories;

public class BookCategoryRepository : GenericRepository<BookCategory>, IBookCategoryRepository
{
    public BookCategoryRepository(AppDbContext appDbContext) : base(appDbContext)
    {

    }
}
