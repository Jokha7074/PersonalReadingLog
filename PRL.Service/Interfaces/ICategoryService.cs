using PRL.Service.DTOs.BookCategories;
using PRL.Service.Helpers;

namespace PRL.Service.Interfaces;

public interface ICategoryService
{
    Task<Response<BookCategoryResultDto>> CreateAsync(BookCategoryCreationDto CreationDto);
    Task<Response<BookCategoryResultDto>> UpdateAsync(BookCategoryUpdateDto UpdateDto);
    Task<Response<bool>> DeleteAsync(long Id);
    Task<Response<BookCategoryResultDto>> GetByIdAsync(long Id);
    Task<Response<List<BookCategoryResultDto>>> GetAllAsync();
    Task<Response<List<BookCategoryResultDto>>> BooksByCategory(long CategoryId);
    Response<List<BookCategoryResultDto>> CategoriesByUser(long UserId);
}
