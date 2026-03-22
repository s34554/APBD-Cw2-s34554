namespace ConsoleApp2;

public class Camera : Device
{
    public required double Megapixels;
    public required string LensMount;
    public Camera(string name, double megapixels, string lensMount, int price, int latefee = 5, bool available = true)
    {
        Name = name;
        Megapixels = megapixels;
        LensMount = lensMount;
        PricePerDay = price;
        LateFeePerDay =  latefee;
        Available = available;
    }

    public override string ToString()
    {
        var availability = Available ? " Available" : " Not Available";
        return Name +
               "MP: " + Megapixels +
               "Lens Mount: " + LensMount +
               "Price: " + PricePerDay +
               "Late fee: " + LateFeePerDay +
               availability;
    }
}