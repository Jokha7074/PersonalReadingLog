using Microsoft.EntityFrameworkCore;
using PRL.Data.Constants;
using PRL.Domain.Entities.Books;
using PRL.Domain.Entities.Users;

namespace PRL.Data.DbContexts;

public class AppDbContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(Constant.ConnectionString);
        optionsBuilder.UseLazyLoadingProxies(true);
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<BookCategory> BookCategories { get; set; }
    public DbSet<Session> Sessions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Book>()
            .HasOne(x => x.User)
            .WithMany(x => x.Books)
            .HasForeignKey(x => x.UserId);

        modelBuilder.Entity<BookCategory>()
            .HasOne(x => x.User)
            .WithMany(x => x.BookCategories)
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<Session>()
            .HasOne(x => x.book)
            .WithOne(x => x.Session);
    }
}
