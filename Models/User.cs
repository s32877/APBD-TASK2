using APBD_TASK2.Enum;

namespace APBD_TASK2.Models;

public class User
{
    public static int nextId = 1;
    
    public int Id { get; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public UserType Type { get; set; }

    public int MaxActiveRentails => Type switch
    {
        UserType.Student => 2,
        UserType.Employee => 5,
        _ => 0
    };

    public User(string name, string surname, UserType type)
    {
        Id = nextId++;
        Name = name;
        Surname = surname;
        Type = type;
    }
}