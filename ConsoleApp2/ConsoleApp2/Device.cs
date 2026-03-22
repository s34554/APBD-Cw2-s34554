namespace ConsoleApp2;

public abstract class Device
{
    public string Id { get; } = Guid.NewGuid().ToString();
    public required string Name { get; set; }
    public int LateFee { get; set; } //per day
    public bool Available { get; set; } =  true;
}