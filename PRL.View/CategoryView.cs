using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using PRL.Service.DTOs.BookCategories;
using PRL.Service.Services;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;

namespace PRL.View;

public class CategoryView
{
    private CategoryService categoryService = new();
    
    public async Task MainPage()
    {
        Console.Clear();
        Console.WriteLine("{1} - AddCateogry\n" +
                          "{2} - GetAllCategory\n" +
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
                Main main = new Main();
                await main.MainPage();
                break;
        }

    }
    public async Task GetAllAsync()
    {
        var result =  await categoryService.GetAllAsync();
        foreach (var category in result.Data)
        {
            Console.WriteLine($"Id: {category.Id} Name: {category.Name}");
        }
        Console.WriteLine("{0} - Back");
        Console.ReadLine();
        await MainPage();
    }

    public async Task Add()
    {
        Console.WriteLine("CategoryName: ");
        string name = Console.ReadLine();

        Console.WriteLine("UserId: ");
        long id = long.Parse(Console.ReadLine());

        BookCategoryCreationDto bookCategoryCreationDto = new()
        {
            Name = name,
            UserId = id,
        };

        await categoryService.CreateAsync(bookCategoryCreationDto);
        
        Console.WriteLine("{0} - Back");
        Console.ReadLine();
        await MainPage();
    }

    public async Task GetById()
    {
        Console.Write("Id: ");
        long id = long.Parse(Console.ReadLine());
        
        var result = await categoryService.GetByIdAsync(id);
        Console.WriteLine(result.Data);

        Console.WriteLine("{0} - Back");
        Console.ReadLine();
        await MainPage();
    }
    
    public async Task Delete()
    {
        Console.WriteLine("Id: ");
        long id = long.Parse(Console.ReadLine());   

        BookService bookService = new BookService();
        var result = await bookService.DeleteAsync(id);
        Console.WriteLine(result.Data);

        Console.WriteLine("{0} - Back");
        Console.ReadLine();
        await MainPage();
    }
}
