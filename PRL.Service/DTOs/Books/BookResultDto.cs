using PRL.Domain.Entities.Books;
using PRL.Domain.Entities.Users;

namespace PRL.Service.DTOs.Books;

public class BookResultDto
{
    public long Id { get; set; }    
    public string Title { get; set; }
    public string Author { get; set; }
    public int TotalPages { get; set; }

    public long UserId { get; set; }
    public User User { get; set; }

    public long CotegoryId { get; set; }
    public BookCategory BookCotegory { get; set; }

    public Session Session { get; set; }
}
