using AutoMapper;
using PRL.Data.IRepositories.Commons;
using PRL.Data.Repositories.Commons;
using PRL.Domain.Entities.Users;
using PRL.Service.DTOs.Users;
using PRL.Service.Helpers;
using PRL.Service.Interfaces;
using PRL.Service.Security;

namespace PRL.Service.Services;

public class UserService : IUserService
{
    private IUnitOfWork UnitOfWork;
    private IMapper Mapper;
    public UserService()
    {
        this.UnitOfWork = new UnitOfWork();
        this.Mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>()));
    }

    public async Task<Response<UserResultDto>> CreateAsync(UserCreationDto CreationDto)
    {
        var existUser = await UnitOfWork.UserRepository.GetByEmailAsync(CreationDto.Email);
        if (existUser is not null)
            return new Response<UserResultDto>
            {
                StatusCode = 403,
                Message = "This user is already exist",
                Data = null
            };

        var user = Mapper.Map<User>(CreationDto);
        await UnitOfWork.UserRepository.AddAsync(user);
        await UnitOfWork.SaveAsync();

        var resultDto = Mapper.Map<UserResultDto>(user);

        return new Response<UserResultDto>
        {
            StatusCode = 200,
            Message = "Success",
            Data = resultDto,
        };
    }

    public async Task<Response<UserResultDto>> UpdateAsync(UserUpdateDto UpdateDto)
    {
        var existUser = await UnitOfWork.UserRepository.GetByIdAsync(UpdateDto.Id);
        if (existUser is null)
            return new Response<UserResultDto>
            {
                StatusCode = 404,
                Message = "This user is not found!",
                Data = null
            };

        var user = Mapper.Map(UpdateDto, existUser);
        UnitOfWork.UserRepository.Modification(user);
        await UnitOfWork.SaveAsync();

        var resultDto = Mapper.Map<UserResultDto>(user);

        return new Response<UserResultDto>
        {
            StatusCode = 200,
            Message = "Success",
            Data = resultDto
        };
    }

    public async Task<Response<bool>> DeleteAsync(long Id)
    {
        var existUser = await UnitOfWork.UserRepository.GetByIdAsync(Id);
        if (existUser is null)
            return new Response<bool>
            {
                StatusCode = 404,
                Message = "This user is not found!",
                Data = false
            };

        existUser.IsDeleted = true;
        UnitOfWork.UserRepository.Modification(existUser);
        await UnitOfWork.SaveAsync();

        return new Response<bool>
        {
            StatusCode = 200,
            Message = "Success",
            Data = true,
        };
    }

    public async Task<Response<UserResultDto>> GetByIdAsync(long Id)
    {
        var existUser = await UnitOfWork.UserRepository.GetByIdAsync(Id);
        if (existUser is null)
            return new Response<UserResultDto>
            {
                StatusCode = 404,
                Message = "This user is not found!",
                Data = null
            };

        var resultDto = Mapper.Map<UserResultDto>(existUser);

        return new Response<UserResultDto>
        {
            StatusCode = 200,
            Message = "Success",
            Data = resultDto
        };
    }

    public async Task<Response<List<UserResultDto>>> GetAllAsync()
    {
        var users = UnitOfWork.UserRepository.GetAll().Where(u => u.IsDeleted == false);

        List<UserResultDto> results = new();

        foreach (var user in users)
        {
            var resultDto = Mapper.Map<UserResultDto>(user);
            results.Add(resultDto);
        }

        return new Response<List<UserResultDto>>
        {
            StatusCode = 200,
            Message = "Success",
            Data = results
        };
    }

    public Task<Response<List<UserResultDto>>> BooksOfUser(long Id)
    {

        throw new NotImplementedException();
    }

    public async Task<Response<UserResultDto>> Register(UserCreationDto userDto)
    {
        var existUser = await UnitOfWork.UserRepository.GetByEmailAsync(userDto.Email);
        if (existUser is not null)
            return new Response<UserResultDto>
            {
                StatusCode = 403,
                Message = "This user is already exist",
                Data = null
            };

        var HashSalt = PasswordHasher.Hash(userDto.Password);
        User user = new User()
        {
            FirstName = userDto.FirstName,
            LastName = userDto.LastName,
            Email = userDto.Email,
            Password = HashSalt.PasswordHash,
            Salt = HashSalt.Salt,
        };

        await UnitOfWork.UserRepository.AddAsync(user);
        await UnitOfWork.SaveAsync();

        UserResultDto resultDto = Mapper.Map<UserResultDto>(user);

        return new Response<UserResultDto>
        {
            StatusCode = 200,
            Message = "Success",
            Data = resultDto,
        };
    }

    public async Task<Response<UserResultDto>> LogIn(UserCreationDto userDto)
    {
        var existUser = await UnitOfWork.UserRepository.GetByEmailAsync(userDto.Email);
        if (existUser is null)
            return new Response<UserResultDto>
            {
                StatusCode = 403,
                Message = "This user is already exist",
                Data = null
            };

        var result = PasswordHasher.Verify(userDto.Password, existUser.Password, existUser.Salt);
        if(result is false)
            return new Response<UserResultDto>
            {
                StatusCode = 404,
                Message = "Password yoki email noto'g'ri",
                Data = null,
            };
        var resultDto = Mapper.Map<UserResultDto>(existUser); 

        return new Response<UserResultDto>
        {
            StatusCode = 200,
            Message = "Success",
            Data = resultDto
        };
    }
}