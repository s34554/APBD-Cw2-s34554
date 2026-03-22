namespace ConsoleApp2;

public class User
{
    public string Id { get; }
    public required string Name { get; set; }
    public required string Type { get; set; }
    public User(string name, string type)
    {
        Id = Guid.NewGuid().ToString();
        Name = name;
        Type = type;
    }
}