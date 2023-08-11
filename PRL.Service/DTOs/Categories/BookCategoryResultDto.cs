using PRL.Domain.Entities.Books;
using PRL.Domain.Entities.Users;

namespace PRL.Service.DTOs.BookCategories;

public class BookCategoryResultDto
{
    public long Id { get; set; }
    public string Name { get; set; }

    public long UserId { get; set; }
}
