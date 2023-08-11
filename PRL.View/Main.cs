using PRL.Service.DTOs.BookCategories;
using PRL.Service.DTOs.Users;
using PRL.Service.Services;

namespace PRL.View;

public class Main
{
    private CategoryService categoryService = new();
    public async Task RegisterPage()
    {
        Console.WriteLine("{1} - Register");
        Console.WriteLine("{2} - LogIn");
        Console.Write(">>> ");
        var num = Console.ReadLine();

        switch (num)
        {
            case "1":
                UserView userView = new();
                var result = await userView.Register();
                if (result.isRegister is true)
                {
                    await categoryService.CreateAsync(new BookCategoryCreationDto { Name = "Fiction", UserId = result.user.Id });
                    await categoryService.CreateAsync(new BookCategoryCreationDto { Name = "Ramantic", UserId = result.user.Id });
                    await categoryService.CreateAsync(new BookCategoryCreationDto { Name = "Mativation", UserId = result.user.Id });
                    await MainPage();
                }
                else
                {
                    Console.WriteLine("Email yoki parol notog'ri");
                    await RegisterPage();
                }
                break;
            case "2":
                UserView userView1 = new();
                var result1 = await userView1.LogIn();
                if (result1.IsRegister is true)
                    await MainPage();
                else
                {
                    Console.WriteLine(result1.Message);
                    await RegisterPage();
                }
                break;
            default:
                await RegisterPage();
                break;
        }
    }

    public async Task MainPage()
    {
        Console.Clear();

        Console.WriteLine("{1} - Category\n" +
                          "{2} - Books\n" +
                          "{3} - Proccess\n" +
                          "{0} - Back\n");
        var num = Console.ReadLine();
            
        switch (num)
        {
            case "1":
                CategoryView categoryView = new();
                await categoryView.MainPage();
                break;
            case "2":
                BookView bookView = new();
                await bookView.MainPage();
                break;
            case "3":
                SessionView sessionView = new();
                await sessionView.MainPage();
                break;
            case "0":
                 await RegisterPage();
                break;
            default:
                await MainPage();
            break;
        }

}
