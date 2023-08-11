using PRL.Service.DTOs.Sessions;
using PRL.Service.Services;

namespace PRL.View;

public class SessionView
{
    private SessionService sessionService = new();
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
        Console.Write("StartDate: ");
        var startData = Console.ReadLine();

        Console.Write("EndDate: ");
        var endData = Console.ReadLine();

        Console.Write("PagesRead: ");
        int pagesRead = int.Parse(Console.ReadLine());

        Console.Write("CurrentPage: ");
        int CurrentPage = int.Parse(Console.ReadLine());


        Console.Write("Progress: ");
        int progress = int.Parse(Console.ReadLine());

        Console.Write("Notes: ");
        string notes = Console.ReadLine();

        SessionCreateDto session = new()
        {
            StartDate = DateTime.Parse(startData),
            EndDate = DateTime.Parse(endData),
            CurrentPage = CurrentPage,
            PagesRead = pagesRead,
            Progress = progress,
            Notes = notes,
        };

        var result = await sessionService.CreateAsync(session);
        await Console.Out.WriteLineAsync(result.Message);

        Console.Write("{0} - Back: ");
        Console.ReadLine();
        await MainPage();
    }

    public async Task GetAllAsync()
    {
        var result = await sessionService.GetAllAsync();
        int i = 1;
        foreach (var item in result.Data)
        {
            Console.Write($"{i})");
            Console.WriteLine(item.StartDate);
            Console.WriteLine(item.EndDate);
            Console.WriteLine(item.CurrentPage);
            Console.WriteLine(item.PagesRead);
            Console.WriteLine(item.Notes);
            i++;
        }

        Console.Write("{0} - Back: ");
        Console.ReadLine();
        await MainPage();
    }

    public async Task GetById()
    {
        Console.WriteLine("Id: ");
        long id = long.Parse(Console.ReadLine());
        var result = await sessionService.GetByIdAsync(id);
        if (result.StatusCode == 200)
        {
            Console.WriteLine($"StartDate: {result.Data.StartDate}\n" +
                             $"EndDate: {result.Data.EndDate}\n" +
                             $"{result.Data.CurrentPage}\n" +
                             $"{result.Data.Progress}\n" +
                             $"{result.Data.PagesRead}\n" +
                             $"{result.Data.Notes}\n");
        }
        else
        {
            Console.WriteLine(result.Message);
        }


        Console.Write("{0} - Back: ");
        Console.ReadLine();
        await MainPage();
    }

    public async Task Delete()
    {
        Console.Write("Id: ");
        long id = long.Parse(Console.ReadLine());

        var result = await sessionService.DeleteAsync(id);
        Console.WriteLine(result.Message);

        Console.Write("{0} - Back: ");
        Console.ReadLine();
        await MainPage();
    }
}
