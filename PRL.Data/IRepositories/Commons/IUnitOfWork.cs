namespace PRL.Data.IRepositories.Commons;

public interface IUnitOfWork : IDisposable
{
    public IUserRepository UserRepository { get; }
    public IBookCategoryRepository BookCategoryRepository { get; }
    public IBookRepository BookRepository { get; }
    public ISessionRepository SessionRepository { get; }
    Task SaveAsync();
}
