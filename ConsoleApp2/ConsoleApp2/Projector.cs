namespace ConsoleApp2;

public class Projector : Device
{
    public required int Lumens;
    public required string Resolution;
    public Projector(string name, int lumens, string resolution, int price, int latefee = 15, bool available = true)
    {
        Name = name;
        Lumens = lumens;
        Resolution = resolution;
        PricePerDay = price;
        LateFeePerDay =  latefee;
        Available = available;
    }

    public override string ToString()
    {
        var availability = Available ? " Available" : " Not Available";
        return Name +
               "Lumens: " + Lumens +
               "Resolution: " + Resolution + 
               "Price: " + PricePerDay +
               "Late fee: " + LateFeePerDay +
               availability;
    }
}