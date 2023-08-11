using PRL.Domain.Commons;
using PRL.Domain.Entities.Users;

namespace PRL.Domain.Entities.Books;
public class BookCategory : Auditable
{
    public string Name { get; set; }

    public long UserId { get; set; }
    public virtual User User { get; set; }

    public virtual ICollection<Book> Books { get; set; }
}



