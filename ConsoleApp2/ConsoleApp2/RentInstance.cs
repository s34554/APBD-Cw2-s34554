namespace ConsoleApp2;

public class RentInstance(Device deviceRented, DateTime returnDate)
{
    public DateTime RentDate { get; init; } = DateTime.Now;
    public DateTime ReturnDate { get; } = returnDate;
    public Device DeviceRented { get; } = deviceRented;
    public bool BackOnTime { get; set; }
}