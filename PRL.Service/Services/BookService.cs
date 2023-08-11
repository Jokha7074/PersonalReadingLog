using AutoMapper;
using PRL.Data.IRepositories.Commons;
using PRL.Data.Repositories.Commons;
using PRL.Domain.Entities.Books;
using PRL.Service.DTOs.Books;
using PRL.Service.Helpers;
using PRL.Service.Interfaces;

namespace PRL.Service.Services;

public class BookService : IBookService
{
    private IUnitOfWork UnitOfWork;
    private IMapper Mapper;

    public BookService()
    {
        this.UnitOfWork = new UnitOfWork();
        this.Mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>()));
    }
    public async Task<Response<BookResultDto>> CreateAsync(BookCreationDto CreationDto)
    {
        var book = Mapper.Map<Book>(CreationDto);
        await UnitOfWork.BookRepository.AddAsync(book);
        await UnitOfWork.SaveAsync();

        var resultDto = Mapper.Map<BookResultDto>(book);

        return new Response<BookResultDto>
        {
            StatusCode = 200,
            Message = "Success",
            Data = resultDto,
        };
    }

    public async Task<Response<bool>> DeleteAsync(long Id)
    {
        var book = await UnitOfWork.BookRepository.GetByIdAsync(Id);
        if (book is null || book.IsDeleted)
            return new Response<bool>()
            {
                StatusCode = 404,
                Message = "Book is not found",
                Data = false,
            };

        book.IsDeleted = true;
        await UnitOfWork.SaveAsync();
        return new Response<bool>()
        {
            StatusCode = 200,
            Message = "Success",
            Data = true,
        };
    }

    public async Task<Response<List<BookResultDto>>> GetAllAsync()
    {
        var books = UnitOfWork.BookRepository.GetAll();
        List<BookResultDto> bookResultDtos = new();

        foreach (var book in books)
        {
            var result = Mapper.Map<BookResultDto>(book);
            bookResultDtos.Add(result);
        }

        return new Response<List<BookResultDto>>
        {
            StatusCode = 200,
            Message = "Success",
            Data = bookResultDtos,
        };
    }

    public async Task<Response<BookResultDto>> GetByIdAsync(long Id)
    {
        var book = await UnitOfWork.BookRepository.GetByIdAsync(Id);
        if (book is null || book.IsDeleted)
            return new Response<BookResultDto>()
            {
                StatusCode = 404,
                Message = "Book is not found",
                Data = null
            };

        var bookResult = Mapper.Map<BookResultDto>(book);

        return new Response<BookResultDto>
        {
            StatusCode = 200,
            Message = "Success",
            Data = bookResult
        };
    }

    public async Task<Response<BookResultDto>> UpdateAsync(BookUpdateDto UpdateDto)
    {
        var book = await UnitOfWork.BookRepository.GetByIdAsync(UpdateDto.Id);
        if (book is null || book.IsDeleted)
            return new Response<BookResultDto>()
            {
                StatusCode = 404,
                Message = "Book is not found",
                Data = null
            };

        var modified = Mapper.Map(UpdateDto, book);
        UnitOfWork.BookRepository.Modification(modified);
        await UnitOfWork.SaveAsync();

        var resultBook = Mapper.Map<BookResultDto>(modified);

        return new Response<BookResultDto>()
        {
            StatusCode = 200,
            Message = "Success",
            Data = resultBook
        };
    }

    public Response<List<BookResultDto>> SearchBooks(string name)
    {
        var result = UnitOfWork.BookRepository.SearchBooks(name);
        List<BookResultDto> bookResults = new();

        foreach (var book in result)
        {
            var mapped = Mapper.Map<BookResultDto>(book);
            bookResults.Add(mapped);
        }

        return new Response<List<BookResultDto>>()
        {
            StatusCode = 200,
            Message = "Success",
            Data = bookResults
        };
    }

    public Response<List<BookResultDto>> BooksByCategory(long categoryId)
    {
        var books = UnitOfWork.BookRepository.GetAll();
        List<BookResultDto> bookResultDtos = new();

        foreach (var book in books)
        {
            if (book.BookCotegoryId == categoryId) {
                var result = Mapper.Map<BookResultDto>(book);
                bookResultDtos.Add(result);
            }
        }

        return new Response<List<BookResultDto>>
        {
            StatusCode = 200,
            Message = "Success",
            Data = bookResultDtos,
        };
    }
}
