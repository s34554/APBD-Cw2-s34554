namespace ConsoleApp2;

public abstract class Device
{
    public string Id { get; } = Guid.NewGuid().ToString();
    public string Name { get; set; }
    public float PricePerDay { get; protected init; }
    public float LateFeePerDay { get; protected init; }
    public bool Available { get; set; }
    
    public string PrintAvailable()
    {
        return Available ? "Available" : "Not Available";
    }
}