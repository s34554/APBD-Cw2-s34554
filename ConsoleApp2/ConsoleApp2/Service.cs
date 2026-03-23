namespace ConsoleApp2;

public class Service
{
    public static List<Device> Devices { get; } = [];
    public List<User> Users { get; } = [];

    public void AddDevice(Device device)
    {
        Devices.Add(device);
    }

    public void AddUser(User user)
    {
        Users.Add(user);
    }
    public void Rent(User user, string deviceName, DateOnly rentDate, DateOnly returnDate)
    {
        if (!Users.Contains(user)) Users.Add(user);
        var foundDevice = Devices.FirstOrDefault(d => d.Name == deviceName);
        if (foundDevice is null)
        {
            Console.WriteLine("Unknown device");
            return;
        }
        if (!foundDevice.Available)
        {
            Console.WriteLine("Device not available");
            return;
        }        
        if (user.ActiveRentCount >= user.MaxActiveRentCount)
        { 
            Console.WriteLine("User has too many active rents");
            return;
        }
        user.RentedDevices.Add(foundDevice);
        user.ActiveRentInstances.Add(new RentInstance(foundDevice, rentDate, returnDate));
        user.ActiveRentCount++;
        foundDevice.Available = false;
            
    }

    public void Return(User user, string deviceName, int daysUsed)
    {
        if (!Users.Contains(user)) Console.WriteLine("Unknown user");
        var foundDevice = user.RentedDevices.FirstOrDefault(d => d.Name == deviceName);
        if (foundDevice is null)
        {
            Console.WriteLine("Unknown device");
            return;
        }
        foundDevice.Available = true;
        user.RentedDevices.Remove(foundDevice);
        var rentInstance = user.ActiveRentInstances.Find(r => r.DeviceRented == foundDevice);
        rentInstance.CalculateTotalPrice(daysUsed);
        user.RentInstancesHistory.Add(rentInstance);
        user.ActiveRentInstances.Remove(rentInstance);
        user.ActiveRentCount--;
    }

    public void MakeUnavailable(string name)
    {
        var foundDevice = Devices.FirstOrDefault(d => d.Name == name);
        if (foundDevice is null)
        {
            Console.WriteLine("Unknown device");
            return;
        }
        foundDevice.Available = false;
        Console.WriteLine(foundDevice.Name + " now marked unavailable");
    }
    public void PrintAll()
    {
        Console.WriteLine("All devices:");
        foreach (var device in Devices) Console.WriteLine(device + "\n");
    }
    public void PrintAvailable()
    {
        Console.WriteLine("Available devices:");
        foreach (var device in Devices.Where(device => device.Available)) Console.WriteLine(device);
    }

    public void PrintActiveRents(User user)
    {
        if (!Users.Contains(user)) Console.WriteLine("Unknown user");
        Console.WriteLine("Active rent instances for user: " + user);
        foreach (var rentInstance in user.ActiveRentInstances)
        {
            Console.WriteLine(rentInstance.OnlyDateAndDevice());
        }
    }

    public void PrintLateRents(User user)
    {
        if (!Users.Contains(user)) Console.WriteLine("Unknown user");
        Console.WriteLine("Late rent instances for user: " + user);
        foreach (var rentInstance in user.RentInstancesHistory.Where(r => r.IsLate()))
        {
            Console.WriteLine(rentInstance);
        }
    }

    public void PrintReport()
    {
        PrintAll();
        Console.WriteLine("Users:");
        foreach (var user in Users)
        {
            Console.WriteLine(user);
        }

        foreach (var user in Users)
        {
            PrintActiveRents(user);
        }
    }
}