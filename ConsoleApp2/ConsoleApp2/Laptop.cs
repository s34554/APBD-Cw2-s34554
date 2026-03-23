namespace ConsoleApp2;

public class Laptop : Device
{
    public string Processor;
    public int RamGb;
    public Laptop(string name, string processor, int ramGb, int price, int latefee, bool available = true)
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
        return Name +
               ", Processor: " + Processor +
               ", RamGb " + RamGb +
               ", Price per day: " + PricePerDay +
               ", Late fee: " + LateFeePerDay;
    }
}