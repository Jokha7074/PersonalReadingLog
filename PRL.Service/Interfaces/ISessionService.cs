using PRL.Service.DTOs.Sessions;
using PRL.Service.Helpers;

namespace PRL.Service.Interfaces;

public interface ISessionService
{
    Task<Response<SessionResultDto>> CreateAsync(SessionCreateDto CreationDto);
    Task<Response<SessionResultDto>> UpdateAsync(SessionUpdateDto UpdateDto);
    Task<Response<bool>> DeleteAsync(long Id);
    Task<Response<SessionResultDto>> GetByIdAsync(long Id);
    Task<Response<List<SessionResultDto>>> GetAllAsync();
}






