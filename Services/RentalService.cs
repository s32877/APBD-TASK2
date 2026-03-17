using APBD_TASK2.Database;
using APBD_TASK2.Enum;
using APBD_TASK2.Interfaces;
using APBD_TASK2.Models;

namespace APBD_TASK2.Services;

public class RentalService : IRentalService
{
    public void AddEquipment(Equipment equipment)
    {
        var db = Singleton.Instance;
        db.EquipmentList.Add(equipment);
    }

    public void AddUser(User user)
    {
        var db = Singleton.Instance;
        db.UserList.Add(user);
    }

    public List<Equipment> GetAvailableEquipment()
    {
        var db = Singleton.Instance;
        return db.EquipmentList.Where(c => c.Status == EquipmentStatus.Available).ToList();
    }

    public List<Equipment> GetAllEquipment()
    {
        var db = Singleton.Instance;
        return db.EquipmentList;
    }
}