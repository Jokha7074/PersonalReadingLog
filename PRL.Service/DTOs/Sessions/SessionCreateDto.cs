namespace PRL.Service.DTOs.Sessions;

public class SessionCreateDto
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int PagesRead { get; set; }
    public int CurrentPage { get; set; }
    public int Progress { get; set; }
    public string Notes { get; set; }

    public long BookId { get; set; }
}
