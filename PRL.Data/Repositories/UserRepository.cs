using Microsoft.EntityFrameworkCore;
using PRL.Data.DbContexts;
using PRL.Data.IRepositories;
using PRL.Data.IRepositories.Commons;
using PRL.Data.Repositories.Commons;
using PRL.Domain.Entities.Users;
using System.Runtime.CompilerServices;

namespace PRL.Data.Repositories;

public class UserRepository : GenericRepository<User> ,IUserRepository
{
    private AppDbContext appDbContext;
    public UserRepository(AppDbContext appDbContext) : base(appDbContext)
    {
        this.appDbContext = appDbContext;
    }

    public async Task<User> GetByEmailAsync(string email)
    {
         var result = await appDbContext.Users.FirstOrDefaultAsync(u=> u.Email.Equals(email));

        return result;
    }
}
