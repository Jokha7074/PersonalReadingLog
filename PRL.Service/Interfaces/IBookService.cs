using PRL.Service.DTOs.Books;
using PRL.Service.Helpers;

namespace PRL.Service.Interfaces;

public interface IBookService
{
    Task<Response<BookResultDto>> CreateAsync(BookCreationDto CreationDto);
    Task<Response<BookResultDto>> UpdateAsync(BookUpdateDto UpdateDto);
    Task<Response<bool>> DeleteAsync(long Id);
    Task<Response<BookResultDto>> GetByIdAsync(long Id);
    Task<Response<List<BookResultDto>>> GetAllAsync();
    Response<List<BookResultDto>> SearchBooks(string name);
    Response<List<BookResultDto>> BooksByCategory(long categoryId);
}
