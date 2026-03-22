namespace ConsoleApp2;

public class Service
{
    public static List<Device> Devices { get; } = [];

    public static void AddDevice(Device d)
    {
        Devices.Add(d);
    }
    public static void Rent(User user, Device device, DateTime returnDate)
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
            
        user.ActiveRentInstances.Add(new RentInstance(device, returnDate));
        user.ActiveRentCount++;
        device.Available = false;
            
    }

    public static void Return(User user, Device device)
    {
        
    }
}