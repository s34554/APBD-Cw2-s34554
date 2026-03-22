namespace ConsoleApp2;

public class Laptop : Device
{
    public required string Processor;
    public required int RamGb;
    public Laptop(string name, string processor, int ramGb, int price, int latefee = 10, bool available = true)
    {
        Name = name;
        Processor = processor;
        RamGb = ramGb;
        PricePerDay = price;
        LateFeePerDay =  latefee;
        Available = available;
    }

    public override string ToString()
    {
        var availability = Available ? " Available" : " Not Available";
        return Name +
               "Processor: " + Processor +
               "RamGb " + RamGb +
               "Price: " + PricePerDay +
               "Late fee: " + LateFeePerDay +
               availability;
    }
}