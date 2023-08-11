using AutoMapper;
using Microsoft.EntityFrameworkCore.Query.Internal;
using PRL.Data.IRepositories.Commons;
using PRL.Data.Repositories.Commons;
using PRL.Domain.Entities.Books;
using PRL.Service.DTOs.Books;
using PRL.Service.DTOs.Sessions;
using PRL.Service.Helpers;
using PRL.Service.Interfaces;

namespace PRL.Service.Services;

public class SessionService : ISessionService
{
    private readonly IMapper Mapper;
    private IUnitOfWork UnitOfWork;

    public SessionService()
    {
        this.UnitOfWork = new UnitOfWork();
        this.Mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>()));
    }

    public async Task<Response<SessionResultDto>> CreateAsync(SessionCreateDto CreationDto)
    {
        var session = Mapper.Map<Session>(CreationDto);
        await UnitOfWork.SessionRepository.AddAsync(session);
        await UnitOfWork.SaveAsync();

        var resultDto = Mapper.Map<SessionResultDto>(session);

        return new Response<SessionResultDto>
        {
            StatusCode = 200,
            Message = "Success",
            Data = resultDto,
        };
    }

    public async Task<Response<bool>> DeleteAsync(long Id)
    {
        var session = await UnitOfWork.SessionRepository.GetByIdAsync(Id);
        if (session is null || session.IsDeleted)
            return new Response<bool>()
            {
                StatusCode = 404,
                Message = "Book is not found",
                Data = false,
            };

        session.IsDeleted = true;
        await UnitOfWork.SaveAsync();
        return new Response<bool>()
        {
            StatusCode = 200,
            Message = "Success",
            Data = true,
        };
    }

    public async Task<Response<List<SessionResultDto>>> GetAllAsync()
    {
        var sessions = UnitOfWork.SessionRepository.GetAll();
        List<SessionResultDto> sessionResultDtos = new();

        foreach (var session in sessions)
        {
            var result = Mapper.Map<SessionResultDto>(session);
            sessionResultDtos.Add(result);
        }

        return new Response<List<SessionResultDto>>
        {
            StatusCode = 200,
            Message = "Success",
            Data = sessionResultDtos,
        };
    }

    public async Task<Response<SessionResultDto>> GetByIdAsync(long Id)
    {
        var session = await UnitOfWork.SessionRepository.GetByIdAsync(Id);
        if (session is null || session.IsDeleted)
            return new Response<SessionResultDto>()
            {
                StatusCode = 404,
                Message = "Session is not found",
                Data = null
            };

        var sessionResult = Mapper.Map<SessionResultDto>(session);

        return new Response<SessionResultDto>
        {
            StatusCode = 200,
            Message = "Success",
            Data = sessionResult
        };
    }

    public async Task<Response<SessionResultDto>> UpdateAsync(SessionUpdateDto UpdateDto)
    {

        var session = await UnitOfWork.SessionRepository.GetByIdAsync(UpdateDto.Id);
        if (session is null || session.IsDeleted)
            return new Response<SessionResultDto>()
            {
                StatusCode = 404,
                Message = "Book is not found",
                Data = null
            };

        var modified = Mapper.Map(UpdateDto, session);
        UnitOfWork.SessionRepository.Modification(modified);
        await UnitOfWork.SaveAsync();

        var sessionResultDto = Mapper.Map<SessionResultDto>(modified);

        return new Response<SessionResultDto>()
        {
            StatusCode = 200,
            Message = "Success",
            Data = sessionResultDto
        };
    }
}
