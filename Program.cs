

using APBD_TASK2.Database;
using APBD_TASK2.Enum;
using APBD_TASK2.Models;
using APBD_TASK2.Services;

var db = Singleton.Instance;
var service = new RentalService();

var laptop = new Laptop("Asus TUF F16", 16 ,13);
var laptop2 = new Laptop("MacBook Pro 14",  32, 14);
var projector = new Projector("Epson EB-X51", 1080, 30, 25, 12);
var projector2 = new Projector("BenQ MH560",   4096, 28, 22, 10);
var camera = new Camera("Canon EOS 250D",  24, 5, 6000);
var camera2 = new Camera("Sony ZV-E10",     24, 4, 5000);

service.AddEquipment(laptop);
service.AddEquipment(laptop2);
service.AddEquipment(projector);
service.AddEquipment(projector2);
service.AddEquipment(camera);
service.AddEquipment(camera2);

var student1  = new User("Anna",  "Kowalska",   UserType.Student);
var student2  = new User("Piotr", "Nowak",      UserType.Student);
var employee1 = new User("Maria", "Wisniewska", UserType.Employee);
//if you are reading this, tell me for the future if I can use just funny placeholder names

service.AddUser(student1);
service.AddUser(student2);
service.AddUser(employee1);
Console.WriteLine("Added 3 users.");

Console.WriteLine("\nAll Equipment: ");
foreach (var e in service.GetAllEquipment())
{
    Console.WriteLine("[" + e.Id + "] " + e.GetType().Name + " - " + e.Name + " | Status: " + e.Status);
}

Console.WriteLine("\nAvailable Equipment: ");
foreach (var e in service.GetAvailableEquipment())
{
    Console.WriteLine("[" + e.Id + "] " + e.GetType().Name + " - " + e.Name);
}

Console.WriteLine("\nRenting Equipment:");
bool rent1 = service.RentEquipment(student1, laptop, RentalPolicy.DefaultRentalDays);
Console.WriteLine(rent1 ? "OK - Anna rented " + laptop.Name : "FAILED - could not rent " + laptop.Name);
bool rent2 = service.RentEquipment(employee1, projector, 14);
Console.WriteLine(rent2 ? "OK - Maria rented " + projector.Name : "FAILED - could not rent " + projector.Name);

service.RentEquipment(student1, camera, 3);
bool failLimit = service.RentEquipment(student1, laptop2, 5);
Console.WriteLine(failLimit ? "OK - rented (unexpected)" : "BLOCKED - Anna exceeded rental limit");
service.MarkUnavailable(projector2);
bool failUnavailable = service.RentEquipment(student2, projector2, 3);
Console.WriteLine(failUnavailable ? "OK - rented (unexpected)" : "BLOCKED - projector2 is unavailable");

Console.WriteLine("\nOn Time Return:");
bool returnedOnTime = service.ReturnEquipment(camera.Id, DateTime.Now);
Console.WriteLine(returnedOnTime ? "OK - camera returned on time" : "FAILED - return unsuccessful");



Console.WriteLine("\nLate Return, 10 days overdue:");
var lateRental = service.GetActiveRentalsForUser(student1.Id)
    .FirstOrDefault(r => r.Equipment.Id == laptop.Id);
if (lateRental != null)
{
    bool returnedLate = service.ReturnEquipment(laptop.Id, lateRental.DueDate.AddDays(10));
    Console.WriteLine(returnedLate ? "OK - laptop returned late: " + lateRental : "FAILED");
}

Console.WriteLine("\nActive Rentals for Anna:");
var annaRentals = service.GetActiveRentalsForUser(student1.Id);
if (annaRentals.Count == 0)
    Console.WriteLine("No active rentals.");
else
    foreach (var r in annaRentals)
        Console.WriteLine(r.ToString());

Console.WriteLine("\nOverdue Rentals:");
var overdue = service.GetOverdueRentals();
if (overdue.Count == 0)
    Console.WriteLine("No overdue rentals.");
else
    foreach (var r in overdue)
        Console.WriteLine(r.ToString());



//report
int totalFines = 0;
foreach (var r in db.RentalList)
{
    if (r.LatePenalty.HasValue)
        totalFines += r.LatePenalty.Value;
}

Console.WriteLine("==========================================");
Console.WriteLine("\n         RENTAL SERVICE REPORT           ");
Console.WriteLine("==========================================");
Console.WriteLine("Users registered  : " + db.UserList.Count);
Console.WriteLine("Equipment items   : " + db.EquipmentList.Count);
Console.WriteLine("  Available       : " + db.EquipmentList.Count(e => e.Status == EquipmentStatus.Available));
Console.WriteLine("  Rented          : " + db.EquipmentList.Count(e => e.Status == EquipmentStatus.Rented));
Console.WriteLine("  Unavailable     : " + db.EquipmentList.Count(e => e.Status == EquipmentStatus.Unavailable));
Console.WriteLine("Total rentals     : " + db.RentalList.Count);
Console.WriteLine("  Active          : " + db.RentalList.Count(r => r.IsActive));
Console.WriteLine("  Overdue         : " + db.RentalList.Count(r => r.IsOverdue));
Console.WriteLine("  Completed       : " + db.RentalList.Count(r => !r.IsActive));
Console.WriteLine("Total penalties   : " + totalFines + " PLN");
Console.WriteLine("==========================================");