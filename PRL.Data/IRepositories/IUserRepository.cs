using PRL.Data.IRepositories.Commons;
using PRL.Domain.Entities.Users;

namespace PRL.Data.IRepositories;

public interface IUserRepository : IGenericRepository<User>
{
    Task<User> GetByEmailAsync(string email);
}
