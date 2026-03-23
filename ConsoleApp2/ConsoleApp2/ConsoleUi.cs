namespace ConsoleApp2;

public class ConsoleUI
{
    private readonly Service _service = new();

    public void Run()
    {
        Console.WriteLine("=== University Equipment Rental System ===\n");
        SeedData();

        bool running = true;
        while (running)
        {
            PrintMenu();
            var choice = Console.ReadLine()?.Trim();
            Console.WriteLine();
            running = HandleChoice(choice);
            Console.WriteLine();
        }
        Console.WriteLine("Goodbye!");
    }

    private void PrintMenu()
    {
        Console.WriteLine("--- MENU ---");
        Console.WriteLine("1.  List all devices");
        Console.WriteLine("2.  List available devices");
        Console.WriteLine("3.  Add new user");
        Console.WriteLine("4.  List users");
        Console.WriteLine("5.  Rent a device");
        Console.WriteLine("6.  Return a device");
        Console.WriteLine("7.  Mark device as unavailable");
        Console.WriteLine("8.  Active rents for user");
        Console.WriteLine("9.  Late rents for user");
        Console.WriteLine("10. Print report");
        Console.WriteLine("0.  Exit");
        Console.Write("\nSelect option: ");
    }

    private bool HandleChoice(string? choice)
    {
        switch (choice)
        {
            case "1": Service.PrintAll(); break;
            case "2": _service.PrintAvailable(); break;
            case "3": AddUser(); break;
            case "4": PrintUsers(); break;
            case "5": RentDevice(); break;
            case "6": ReturnDevice(); break;
            case "7": MakeUnavailable(); break;
            case "8": PrintActiveRentsForUser(); break;
            case "9": PrintLateRentsForUser(); break;
            case "10": _service.PrintReport(); break;
            case "0": return false;
            default: Console.WriteLine("Unknown option."); break;
        }
        return true;
    }

    //Helpers

    private void AddUser()
    {
        Console.Write("First name: ");
        var name = Console.ReadLine()?.Trim() ?? "";

        Console.Write("Last name: ");
        var lastName = Console.ReadLine()?.Trim() ?? "";

        Console.Write("Type (student / worker): ");
        var typeInput = Console.ReadLine()?.Trim().ToLower();
        var type = typeInput == "worker" ? UserType.Worker : UserType.Student;

        var user = new User(name, lastName, type);
        _service.AddUser(user);
        Console.WriteLine($"Added user: ({user})");
    }

    private void PrintUsers()
    {
        var users = _service.Users;
        if (users.Count == 0) { Console.WriteLine("No users found."); return; }
        Console.WriteLine("Users:");
        foreach (var u in users) Console.WriteLine("  " + u);
    }

    private void RentDevice()
    {
        var user = SelectUser();
        if (user is null) return;

        Console.Write("Device name: ");
        var deviceName = Console.ReadLine()?.Trim() ?? "";

        Console.Write("Rent date (YYYY-MM-DD, Enter = today): ");
        var rentInput = Console.ReadLine()?.Trim();
        var rentDate = string.IsNullOrEmpty(rentInput)
            ? DateOnly.FromDateTime(DateTime.Now)
            : DateOnly.Parse(rentInput);

        Console.Write("Planned return date (YYYY-MM-DD): ");
        var returnInput = Console.ReadLine()?.Trim() ?? "";
        var returnDate = DateOnly.Parse(returnInput);

        Print(_service.Rent(user, deviceName, rentDate, returnDate));
    }

    private void ReturnDevice()
    {
        var user = SelectUser();
        if (user is null) return;

        if (user.ActiveRentInstances.Count == 0)
        {
            Console.WriteLine("This user has no active rents.");
            return;
        }

        Console.WriteLine("Active rents:");
        foreach (var r in user.ActiveRentInstances)
            Console.WriteLine("  " + r.OnlyDateAndDevice());

        Console.Write("Device name to return: ");
        var deviceName = Console.ReadLine()?.Trim() ?? "";

        Console.Write("How many days was it actually used: ");
        if (!int.TryParse(Console.ReadLine(), out var daysUsed))
        {
            Console.WriteLine("Invalid number of days.");
            return;
        }

        Print(_service.Return(user, deviceName, daysUsed));
    }

    private void MakeUnavailable()
    {
        Console.Write("Device name: ");
        var name = Console.ReadLine()?.Trim() ?? "";
        Print(_service.MakeUnavailable(name));
    }

    private static void Print(OperationResult result)
    {
        var prefix = result.Success ? "[OK]" : "[ERROR]";
        Console.WriteLine($"{prefix} {result.Message}");
    }

    private void PrintActiveRentsForUser()
    {
        var user = SelectUser();
        if (user is null) return;
        _service.PrintActiveRents(user);
    }

    private void PrintLateRentsForUser()
    {
        var user = SelectUser();
        if (user is null) return;
        _service.PrintLateRents(user);
    }

    private User? SelectUser()
    {
        var users = _service.Users;
        if (users.Count == 0) { Console.WriteLine("No users in the system."); return null; }

        Console.WriteLine("Select a user:");
        for (int i = 0; i < users.Count; i++)
            Console.WriteLine($"  {i + 1}. {users[i]}");

        Console.Write("Number: ");
        if (!int.TryParse(Console.ReadLine(), out var idx) || idx < 1 || idx > users.Count)
        {
            Console.WriteLine("Invalid selection.");
            return null;
        }
        return users[idx - 1];
    }

    //Seed data

    private void SeedData()
    {
        _service.AddDevice(new Laptop("Dell Pro Max 18 Plus", "Intel Core Ultra 9 285HX", 128, 150, 45));
        _service.AddDevice(new Laptop("ASUS Zenbook DUO 2026", "Intel Core Ultra X9", 32, 120, 30));
        _service.AddDevice(new Laptop("MacBook Pro 16 M5", "Apple M5 Max", 64, 160, 50));
        _service.AddDevice(new Projector("Sony Bravia Projector 8", 2700, "4K Native", 200, 75));
        _service.AddDevice(new Projector("Epson EH-LS9000", 2200, "4K Pixel-Shift", 110, 30));
        _service.AddDevice(new Projector("Hisense C2 Ultra", 3000, "4K Triple Laser", 80, 25));
        _service.AddDevice(new Camera("Sony A1 II", 50.0, "Sony E-Mount", 180, 60));
        _service.AddDevice(new Camera("Canon EOS R5 Mark II", 45.0, "Canon RF-Mount", 140, 45));
        _service.AddDevice(new Camera("Sony A7R V", 61.0, "Sony E-Mount", 130, 40));
        _service.AddUser(new User("Jan", "Kowalski", UserType.Student));
        _service.AddUser(new User("Anna", "Nowak", UserType.Worker));
        Console.WriteLine("Test data loaded.\n");
    }
}
