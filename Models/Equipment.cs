using APBD_TASK2.Enum;

namespace APBD_TASK2.Models;

public abstract class Equipment
{
    private static int nextId = 1;
    public int Id { get;  }
    public string Name { get;  }
    public string Description { get;  }
    public EquipmentStatus Status { get; set; }
    
    public DateTime AddedDate { get; set; }

    public Equipment(string name, string description = "")
    {
        Id = nextId++;
        Name = name;
        Description = description;
        AddedDate = DateTime.Now;
        Status = EquipmentStatus.Available;
    }
}