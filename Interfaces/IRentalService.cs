using APBD_TASK2.Models;

namespace APBD_TASK2.Interfaces;

public interface IRentalService
{
    void AddUser(User user);
    void AddEquipment(Equipment equipment);
    List<Equipment> GetAllEquipment();
    List<Equipment> GetAvailableEquipment();
    bool RentEquipment(User user, Equipment equipment, int days);
    bool ReturnEquipment(int equipmentId, DateTime returnDate);
    void MarkUnavailable(Equipment equipment);
    List<Rental> GetActiveRentalsForUser(int userId);
    List<Rental> GetOverdueRentals();
}