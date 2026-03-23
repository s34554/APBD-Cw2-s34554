namespace ConsoleApp2;

public class RentInstance(Device deviceRented, DateOnly rentDate, DateOnly returnDate)
{
    public DateOnly RentDate { get; init; } = rentDate;
    public DateOnly ReturnDate { get; } = returnDate;
    public Device DeviceRented { get; } = deviceRented;
    public float TotalPrice { get; set; }
    public bool BackOnTime {get; set;}

    public void CalculateTotalPrice(int daysUsed)
    {
        var baseDays = ReturnDate.DayNumber - RentDate.DayNumber;
        var lateDays = 0;
        if (daysUsed - baseDays > 0)
        {
            lateDays = daysUsed - baseDays;
            BackOnTime = false;
        }
        else
        {
            BackOnTime = true;
        }
        TotalPrice =  baseDays * DeviceRented.PricePerDay + lateDays * DeviceRented.LateFeePerDay;
    }

    public string OnlyDateAndDevice()
    {
        return "Rent date: " + RentDate + 
               ", Return date: " + ReturnDate + 
               ", Device rented: (" + DeviceRented + ")";
    }
    public override string ToString()
    {
        return "Rent date: " + RentDate +
               ", Return date: " + ReturnDate +
               ", Device rented: (" + DeviceRented + ")" +
               ", Total price: " + TotalPrice +
               ", Back on time: " + BackOnTime;
    }

    public bool IsLate()
    {
        var currentDate = DateOnly.FromDateTime(DateTime.Now);
        return currentDate >= RentDate && currentDate <= ReturnDate;
    }
}