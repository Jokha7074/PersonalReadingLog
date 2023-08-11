using Castle.DynamicProxy.Generators;
using PRL.Data.Repositories;
using PRL.Service.DTOs.Books;
using PRL.Service.DTOs.Users;
using PRL.Service.Services;
using System.Security.Cryptography.X509Certificates;

namespace PRL.View;

public class BookView
{
    private readonly BookService bookService = new();
    public async Task MainPage()
    {

        Console.Clear();
        Console.WriteLine("{1} - Add\n" +
                          "{2} - GetAll\n" +
                          "{3} - GetById\n" +
                          "{4} - Delete\n" +
                          "{5} - Back");
        Console.Write(">>> ");
        var num = Console.ReadLine();
        switch (num)
        {
            case "1":
                await Add();
                break;
            case "2":
                await GetAllAsync();
                break;
            case "3":
                await GetById();
                break;
            case "4":
                await Delete();
                break;
            case "5":

                break;
        }

    }
    public async Task Add()
    {
        Console.Write("Title: ");
        string title = Console.ReadLine();

        Console.Write("Author: ");
        string author = Console.ReadLine();

        Console.Write("TotalPages: ");
        int totalPage = int.Parse(Console.ReadLine());

        Console.Write("CategoryId: ");
        long categoryId = long.Parse(Console.ReadLine());

        Console.Write("UserId: ");
        long userId = long.Parse(Console.ReadLine());

        BookCreationDto bookCreationDto = new BookCreationDto()
        {
            Author = author,
            Title = title,
            TotalPages = totalPage,
            BookCotegoryId = categoryId,
            UserId = userId
        };

        var result =  await bookService.CreateAsync(bookCreationDto);
        Console.Write(result.Message);

        Console.Write("{0} - Back");
        Console.ReadLine();
        await MainPage();
    }

    public async Task GetAllAsync()
    {
        var result = await bookService.GetAllAsync();

        int i = 1;
        foreach(var item in result.Data)
        {
            Console.Write($"{i})");
            Console.WriteLine(item.Title);
            Console.WriteLine(item.Author);
            Console.WriteLine(item.TotalPages);
            i++;
        };

        Console.WriteLine("{0} - Back");
        Console.ReadLine();
        await MainPage();
    }
    public async Task GetById()
    {
        Console.Write("Id: ");
        long id = long.Parse(Console.ReadLine());

        var result = await bookService.GetByIdAsync(id);
        Console.WriteLine(result.Message);

        Console.WriteLine("{0} - Back");
        Console.ReadLine();
        await MainPage();
    }

    public async Task Delete()
    {
        Console.Write("Id: ");
        long id  = long.Parse(Console.ReadLine());

        var result = await bookService.DeleteAsync(id);

        Console.WriteLine(result.Message);


        Console.WriteLine("{0} - Back");
        Console.ReadLine();
        await MainPage();
    }

}
