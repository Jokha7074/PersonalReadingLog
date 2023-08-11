using PRL.Domain.Entities.Users;
using PRL.Service.DTOs.Users;
using PRL.Service.Helpers;

namespace PRL.Service.Interfaces;

public interface IUserService
{
    Task<Response<UserResultDto>> CreateAsync(UserCreationDto CreationDto);
    Task<Response<UserResultDto>> UpdateAsync(UserUpdateDto UpdateDto);
    Task<Response<bool>> DeleteAsync(long Id);
    Task<Response<UserResultDto>> GetByIdAsync(long Id);
    Task<Response<List<UserResultDto>>> GetAllAsync();
    Task<Response<List<UserResultDto>>> BooksOfUser(long Id);
    Task<Response<UserResultDto>> Register(UserCreationDto user);
    Task<Response<UserResultDto>> LogIn(UserCreationDto user);
}




