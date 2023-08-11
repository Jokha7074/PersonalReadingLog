using PRL.Domain.Commons;
using PRL.Domain.Entities.Users;

namespace PRL.Domain.Entities.Books;
public class Book : Auditable
{
    public string Title { get; set; }
    public string Author { get; set; }
    public int TotalPages { get; set; }

    public long UserId { get; set; }
    public virtual User User { get; set; }

    public long BookCotegoryId { get; set; }
    public virtual BookCategory BookCotegory { get; set;}
    
    public virtual Session Session { get; set; }
}
