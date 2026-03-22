namespace ConsoleApp2;

public class User(string name, string lastname, UserType type)
{
    public string Id { get; } = Guid.NewGuid().ToString();
    public required string Name { get; set; } = name;
    public required string LastName { get; set; } = lastname;
    public required UserType Type { get; set; } = type;
    public List<RentInstance> ActiveRentInstances { get; } = [];
    public List<RentInstance> RentInstancesHistory { get; } = [];
    public int ActiveRentCount { get; set; } = 0;
    public int MaxActiveRentCount { get; } = type switch
    {
        UserType.Student => 2,
        UserType.Worker => 5,
        _ => 0
    };
}
public enum UserType
{
    Student,
    Worker
}