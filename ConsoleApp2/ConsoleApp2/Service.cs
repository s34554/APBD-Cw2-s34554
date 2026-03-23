namespace ConsoleApp2;

public class Service
{
    public static List<Device> Devices { get; } = [];
    public List<User> Users { get; } = [];

    public void AddDevice(Device device) => Devices.Add(device);
    public void AddUser(User user) => Users.Add(user);

    public OperationResult Rent(User user, string deviceName, DateOnly rentDate, DateOnly returnDate)
    {
        if (!Users.Contains(user)) Users.Add(user);

        var device = Devices.FirstOrDefault(d => d.Name == deviceName);
        if (device is null)
            return OperationResult.Fail($"Unknown device: '{deviceName}'");

        if (!device.Available)
            return OperationResult.Fail($"Device '{deviceName}' is not available");

        if (user.ActiveRentCount >= user.MaxActiveRentCount)
            return OperationResult.Fail($"User has reached the limit of {user.MaxActiveRentCount} active rents");

        user.RentedDevices.Add(device);
        user.ActiveRentInstances.Add(new RentInstance(device, rentDate, returnDate));
        user.ActiveRentCount++;
        device.Available = false;

        return OperationResult.Ok($"Rented '{deviceName}' to {user.Name} {user.LastName}");
    }

    public OperationResult Return(User user, string deviceName, int daysUsed)
    {
        if (!Users.Contains(user))
            return OperationResult.Fail("Unknown user");

        var device = user.RentedDevices.FirstOrDefault(d => d.Name == deviceName);
        if (device is null)
            return OperationResult.Fail($"User does not have '{deviceName}' rented");

        var rentInstance = user.ActiveRentInstances.Find(r => r.DeviceRented == device);
        if (rentInstance is null)
            return OperationResult.Fail("Rent instance not found");

        rentInstance.CalculateTotalPrice(daysUsed);
        device.Available = true;
        user.RentedDevices.Remove(device);
        user.ActiveRentInstances.Remove(rentInstance);
        user.RentInstancesHistory.Add(rentInstance);
        user.ActiveRentCount--;

        var lateInfo = rentInstance.BackOnTime ? "returned on time" : "returned late, late fee applied";
        return OperationResult.Ok($"Returned '{deviceName}' — {lateInfo}, total: {rentInstance.TotalPrice}");
    }

    public OperationResult MakeUnavailable(string deviceName)
    {
        var device = Devices.FirstOrDefault(d => d.Name == deviceName);
        if (device is null)
            return OperationResult.Fail($"Unknown device: '{deviceName}'");

        device.Available = false;
        return OperationResult.Ok($"'{deviceName}' marked as unavailable");
    }

    public static void PrintAll()
    {
        Console.WriteLine("All devices:");
        foreach (var d in Devices)
            Console.WriteLine($"  {d}  [{d.PrintAvailable()}]");
    }

    public void PrintAvailable()
    {
        Console.WriteLine("Available devices:");
        var available = Devices.Where(d => d.Available).ToList();
        if (available.Count == 0) Console.WriteLine("  No devices available.");
        foreach (var d in available) Console.WriteLine($"  {d}");
    }

    public void PrintActiveRents(User user)
    {
        Console.WriteLine($"Active rents ({user.Name} {user.LastName}):");
        if (user.ActiveRentInstances.Count == 0) { Console.WriteLine("  None."); return; }
        foreach (var r in user.ActiveRentInstances)
            Console.WriteLine($"  {r.OnlyDateAndDevice()}");
    }

    public void PrintLateRents(User user)
    {
        Console.WriteLine($"Late rents ({user.Name} {user.LastName}):");
        var late = user.RentInstancesHistory.Where(r => !r.BackOnTime).ToList();
        if (late.Count == 0) { Console.WriteLine("  None."); return; }
        foreach (var r in late) Console.WriteLine($"  {r}");
    }

    public void PrintReport()
    {
        Console.WriteLine("========== REPORT ==========");
        PrintAll();
        Console.WriteLine("\nUsers:");
        foreach (var u in Users) Console.WriteLine($"  {u}");
        Console.WriteLine("\nActive rents:");
        foreach (var u in Users) PrintActiveRents(u);
        Console.WriteLine("============================");
    }
}
