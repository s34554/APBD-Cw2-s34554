namespace ConsoleApp2;

public class Service
{
    public static List<Device> Devices { get; } = [];
    
    public static void Rent(User user, Device device, DateOnly returnDate)
    {
        if (!Devices.Contains(device))
        {
            Console.WriteLine("Unknown device");
            return;
        }
        
        if (user.ActiveRentCount >= user.MaxActiveRentCount)
        { 
            Console.WriteLine("User has too many active rents");
            return;
        }

        if (!device.Available)
        {
            Console.WriteLine("Device not available");
            return;
        }
        user.RentedDevices.Add(device);
        user.ActiveRentInstances.Add(new RentInstance(device, returnDate));
        user.ActiveRentCount++;
        device.Available = false;
            
    }

    public static void Return(User user, Device device, int daysUsed)
    {
        if (!user.RentedDevices.Contains(device))
        {
            Console.WriteLine("Unknown device");
            return;
        }
        user.RentedDevices.Remove(device);
        var rentInstance = user.ActiveRentInstances.Find(r => r.DeviceRented == device);
        rentInstance.CalculateTotalPrice(daysUsed);
        user.RentInstancesHistory.Add(rentInstance);
        user.ActiveRentInstances.Remove(rentInstance);
        user.ActiveRentCount--;
    }

    public static void MakeUnavailable(string name)
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
    public static void PrintAll()
    {
        foreach (var device in Devices) Console.WriteLine(device + "\n");
    }
    public static void PrintAvailable()
    {
        foreach (var device in Devices.Where(device => device.Available)) Console.WriteLine(device);
    }
}