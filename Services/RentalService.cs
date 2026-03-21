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
        return db.EquipmentList.ToList();
    }
    public bool RentEquipment(User user, Equipment equipment, int days)
    {
        var db = Singleton.Instance;
        int activeCount = db.RentalList.Count(r => r.User.Id == user.Id && r.IsActive);

        if (equipment.Status != EquipmentStatus.Available)
            return false;

        if (activeCount >= user.MaxActiveRentails)
            return false;

        var rental = new Rental(user, equipment, DateTime.Now, days);
        equipment.Status = EquipmentStatus.Rented;
        db.RentalList.Add(rental);
        return true;
    }

    public bool ReturnEquipment(int equipmentId, DateTime returnDate)
    {
        var db = Singleton.Instance;
        var rental = db.RentalList.FirstOrDefault(r => r.Equipment.Id == equipmentId && r.IsActive);

        if (rental == null)
            return false;

        rental.CompleteReturn(returnDate);
        rental.Equipment.Status = EquipmentStatus.Available;
        return true;
    }

    public void MarkUnavailable(Equipment equipment)
    {
        equipment.Status = EquipmentStatus.Unavailable;
    }

    public List<Rental> GetActiveRentalsForUser(int userId)
    {
        var db = Singleton.Instance;
        return db.RentalList.Where(r => r.User.Id == userId && r.IsActive).ToList();
    }

    public List<Rental> GetOverdueRentals()
    {
        var db = Singleton.Instance;
        return db.RentalList.Where(r => r.IsOverdue).ToList();
    }
}