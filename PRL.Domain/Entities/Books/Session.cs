using PRL.Domain.Commons;

namespace PRL.Domain.Entities.Books;

public class Session : Auditable
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int PagesRead { get; set; }
    public int CurrentPage {get; set;}
    public int Progress { get; set; }
    public string Notes { get; set; }

    public long BookId { get; set; }
    public virtual Book book { get; set; }
}