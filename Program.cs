

using APBD_TASK2.Database;
using APBD_TASK2.Models;

var db = Singleton.Instance;

var laptop = new Laptop("Asus TUF F16", 2 ,13);
db.EquipmentList.Add(laptop);