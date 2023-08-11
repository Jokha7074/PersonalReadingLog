        using PRL.Domain.Commons;
using PRL.Domain.Entities.Books;
using System.ComponentModel.DataAnnotations;

namespace PRL.Domain.Entities.Users;
public class User : Auditable
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public bool EmailComfirimed { get; set; }
    public string Password { get; set; }
    public string Salt { get; set; }

    public virtual ICollection<Book> Books { get; set; }
    public virtual ICollection<BookCategory> BookCategories { get; set; }
}
