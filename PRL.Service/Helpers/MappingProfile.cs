using AutoMapper;
using PRL.Domain.Entities.Books;
using PRL.Domain.Entities.Users;
using PRL.Service.DTOs.BookCategories;
using PRL.Service.DTOs.Books;
using PRL.Service.DTOs.Sessions;
using PRL.Service.DTOs.Users;

namespace PRL.Service.Helpers;
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        //User
        CreateMap<User,UserCreationDto>().ReverseMap();
        CreateMap<User, UserResultDto>().ReverseMap();
        CreateMap<User, UserUpdateDto>().ReverseMap();

        //Book
        CreateMap<Book, BookCreationDto>().ReverseMap();
        CreateMap<Book,BookResultDto>().ReverseMap();   
        CreateMap<Book,BookUpdateDto>().ReverseMap();

        //Book Category
        CreateMap<BookCategory,BookCategoryCreationDto>().ReverseMap();
        CreateMap<BookCategory, BookCategoryResultDto>().ReverseMap();
        CreateMap<BookCategory, BookCategoryUpdateDto>().ReverseMap();

        //Session 
        CreateMap<Session,SessionCreateDto>().ReverseMap();
        CreateMap<Session,SessionUpdateDto>().ReverseMap();
        CreateMap<Session, SessionResultDto>().ReverseMap();
    }
}
