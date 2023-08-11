using PRL.Domain.Entities.Books;

namespace PRL.Service.DTOs.Users;

public class UserResultDto
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }

    public ICollection<Book> Books { get; set; }
    public ICollection<BookCategory> BookCategories { get; set; }
}
