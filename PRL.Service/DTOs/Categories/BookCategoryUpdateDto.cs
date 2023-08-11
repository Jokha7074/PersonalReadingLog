using PRL.Domain.Entities.Books;
using PRL.Domain.Entities.Users;

namespace PRL.Service.DTOs.BookCategories;

public class BookCategoryUpdateDto
{
    public long Id { get; set; }
    public string Name { get; set; }

    public long UserId { get; set; }
    public User User { get; set; }
    
    public ICollection<Book> Books { get; set; }
}