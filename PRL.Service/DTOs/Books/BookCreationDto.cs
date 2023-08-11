namespace PRL.Service.DTOs.Books;

public class BookCreationDto
{
    public string Title { get; set; }
    public string Author { get; set; }
    public int TotalPages { get; set; }

    public long UserId { get; set; }
    public long BookCotegoryId { get; set; }
}
