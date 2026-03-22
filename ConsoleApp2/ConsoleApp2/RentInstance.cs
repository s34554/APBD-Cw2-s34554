namespace ConsoleApp2;

public class RentInstance
{
    public DateTime RentDate { get; init; } = DateTime.Now;
    public DateTime ReturnDate { get; }
    public Device DeviceRented { get; }
    public User UserRentee { get; }
    public bool BackOnTime { get; set; }

    public RentInstance(Device deviceRented, User userRentee,  DateTime returnDate)
    {
        DeviceRented = deviceRented;
        UserRentee = userRentee;
        ReturnDate = returnDate;
    }
    
}