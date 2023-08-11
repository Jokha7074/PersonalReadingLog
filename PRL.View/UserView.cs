using PRL.Service.DTOs.Users;
using PRL.Service.Services;

namespace PRL.View;

public class UserView
{
    public async Task<(UserResultDto user, bool isRegister, string message)> Register()
    {
        Console.Write("FirstName: ");
        string firstName = Console.ReadLine();

        Console.Write("LastName: ");
        string lastName = Console.ReadLine();

        Console.Write("Email: ");
        string email = Console.ReadLine();

        Console.Write("Password: ");
        string password = Console.ReadLine();

        UserCreationDto userCreationDto = new()
        {
            FirstName = firstName,
            LastName = lastName,
            Password = password,
            Email = email
        };

        UserService userService = new UserService();
        var result = await userService.Register(userCreationDto);

        if (result.StatusCode == 200)
        {
            return (result.Data, true, result.Message);
        }

        return (result.Data, false, result.Message);
    }
    public async Task<(UserResultDto user, bool IsRegister, string Message)> LogIn()
    {
        Console.Write("Email: ");
        string email = Console.ReadLine();

        Console.Write("Password: ");
        string password = Console.ReadLine();

        UserCreationDto userCreationDto = new()
        {
            Password = password,
            Email = email
        };

        UserService userService = new UserService();

        var result = await userService.LogIn(userCreationDto);
        if (result.StatusCode == 200)
            return (result.Data, true, result.Message);
        return (result.Data, false, result.Message);
    }
}
