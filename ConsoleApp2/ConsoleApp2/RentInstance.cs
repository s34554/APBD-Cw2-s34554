namespace ConsoleApp2;

public class RentInstance(Device deviceRented, DateOnly returnDate)
{
    public DateOnly RentDate { get; init; } = DateOnly.FromDateTime(DateTime.Now);
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
}