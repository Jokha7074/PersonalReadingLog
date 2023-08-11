using AutoMapper;
using PRL.Data.Repositories.Commons;
using PRL.Domain.Entities.Books;
using PRL.Domain.Entities.Users;
using PRL.Service.DTOs.BookCategories;
using PRL.Service.DTOs.Books;
using PRL.Service.Helpers;
using PRL.Service.Interfaces;

namespace PRL.Service.Services;

public class CategoryService : ICategoryService
{
    private UnitOfWork UnitOfWork;
    private IMapper Mapper;

    public CategoryService()
    {
        this.UnitOfWork = new UnitOfWork();
        this.Mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>()));
    }

    public async Task<Response<BookCategoryResultDto>> CreateAsync(BookCategoryCreationDto CreationDto)
    {
        var category = Mapper.Map<BookCategory>(CreationDto);
        await UnitOfWork.BookCategoryRepository.AddAsync(category);
        await UnitOfWork.SaveAsync();

        var resultDto = Mapper.Map<BookCategoryResultDto>(category);

        return new Response<BookCategoryResultDto>
        {
            StatusCode = 200,
            Message = "Success",
            Data = resultDto,
        };
    }

    public async Task<Response<bool>> DeleteAsync(long Id)
    {
        var category = await UnitOfWork.BookCategoryRepository.GetByIdAsync(Id);
        if (category is null)
            return new Response<bool>()
            {
                StatusCode = 404,
                Message = "This Category is not found!",
                Data = false,
            };
        category.IsDeleted = true;
        await UnitOfWork.SaveAsync();

        return new Response<bool>()
        {
            StatusCode = 200,
            Message = "Success",
            Data = true,
        };
    }

    public async Task<Response<BookCategoryResultDto>> UpdateAsync(BookCategoryUpdateDto UpdateDto)
    {
        var category = await UnitOfWork.BookCategoryRepository.GetByIdAsync(UpdateDto.Id);
        if (category is null)
            return new Response<BookCategoryResultDto>()
            {
                StatusCode = 404,
                Message = "This Category is not found!",
                Data = null,
            };

        var mappedCateogory = Mapper.Map(UpdateDto, category);
        mappedCateogory.UpdatedAt = DateTime.UtcNow;

        UnitOfWork.BookCategoryRepository.Modification(mappedCateogory);
        await UnitOfWork.SaveAsync();

        var resultDto = Mapper.Map<BookCategoryResultDto>(mappedCateogory);

        return new Response<BookCategoryResultDto>
        {
            StatusCode = 200,
            Message = "Success",
            Data = resultDto,
        };
    }

    public async Task<Response<List<BookCategoryResultDto>>> GetAllAsync()
    {

        var result = UnitOfWork.BookCategoryRepository.GetAll();

        List<BookCategoryResultDto> categoryResultDtos = new();



        foreach (var category in result)
        {
            if (category is null) continue;

            var categoryR = Mapper.Map<BookCategoryResultDto>(category);
            categoryResultDtos.Add(categoryR);
        };

        return new Response<List<BookCategoryResultDto>>
        {
            StatusCode = 200,
            Data = categoryResultDtos,
            Message = "Success"
        };

    }

    public async Task<Response<BookCategoryResultDto>> GetByIdAsync(long Id)
    {
        var category = await UnitOfWork.BookCategoryRepository.GetByIdAsync(Id);
        if (category is null || category.IsDeleted)
            return new Response<BookCategoryResultDto>()
            {
                StatusCode = 404,
                Message = "This Category is not found!",
                Data = null,
            };

        var resultDto = Mapper.Map<BookCategoryResultDto>(category);

        return new Response<BookCategoryResultDto>
        {
            StatusCode = 200,
            Message = "Success",
            Data = resultDto,
        };
    }

    public async Task<Response<List<BookCategoryResultDto>>> BooksByCategory(long CategoryId)
    {
        var categories = UnitOfWork.BookCategoryRepository.GetAll();
        List<BookCategoryResultDto> cateogryResultDtos = new();

        foreach (var category in categories)
        {
            var result = Mapper.Map<BookCategoryResultDto>(category);
            cateogryResultDtos.Add(result);
        }

        return new Response<List<BookCategoryResultDto>>
        {
            StatusCode = 200,
            Message = "Success",
            Data = cateogryResultDtos,
        };
    }

    public Response<List<BookCategoryResultDto>> CategoriesByUser(long UserId)
    {
        var result = UnitOfWork.BookCategoryRepository.GetAll();

        List<BookCategoryResultDto> categoryResultDtos = new();

        foreach(var category in result)
        {
            if (category.UserId == UserId && category is null)
            {
                var categoryR = Mapper.Map<BookCategoryResultDto>(category);
                categoryResultDtos.Add(categoryR);
            }        
        };

        return new Response<List<BookCategoryResultDto>>
        {
            StatusCode = 200,
            Data = categoryResultDtos,
            Message = "Success"
        };

    }
}
