namespace ConsoleApp2;

public class Projector : Device
{
    public int Lumens;
    public string Resolution;
    public Projector(string name, int lumens, string resolution, int price, int latefee, bool available = true)
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
        return Name +
               ", Lumens: " + Lumens +
               ", Resolution: " + Resolution + 
               ", Price per day: " + PricePerDay +
               ", Late fee: " + LateFeePerDay;
    }
}