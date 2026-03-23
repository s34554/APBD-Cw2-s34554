namespace ConsoleApp2;

public class Camera : Device
{
    public double Megapixels;
    public string LensMount;
    public Camera(string name, double megapixels, string lensMount, int price, int latefee, bool available = true)
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
        return Name +
               ", MP: " + Megapixels +
               ", Lens Mount: " + LensMount +
               ", Price per day: " + PricePerDay +
               ", Late fee: " + LateFeePerDay;
    }
}