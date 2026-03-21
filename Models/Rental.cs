using APBD_TASK2.Services;

namespace APBD_TASK2.Models;

public class Rental
{
    private static int nextId = 1;

    public int Id { get; }
    public User User { get; }
    public Equipment Equipment { get; }
    public DateTime RentedAt { get; }
    public DateTime DueDate { get; }
    public DateTime? ReturnedAt { get; private set; }
    public int? LatePenalty { get; private set; }

    public bool IsActive => ReturnedAt == null;
    public bool IsOverdue => IsActive && DateTime.Now > DueDate;
    
    public Rental(User user, Equipment equipment, DateTime rentedAt, int rentalDays)
    {
        Id = nextId++;
        User = user;
        Equipment = equipment;
        RentedAt = rentedAt;
        DueDate = rentedAt.AddDays(rentalDays);
    }

    public void CompleteReturn(DateTime returnedAt)
    {
        ReturnedAt = returnedAt;
        if (returnedAt > DueDate)
        {
            int daysLate = (int)(returnedAt - DueDate).TotalDays + 1;
            LatePenalty = RentalPolicy.CalculatePenalty(daysLate);
        }
        else
        {
            LatePenalty = 0;
        }
    }
    public override string ToString()
    {
        string status;
        if (!IsActive)
        {
            status = "Returned on " + ReturnedAt?.ToString("yyyy-MM-dd");
        }
        else if (IsOverdue)
        {
            status = "OVERDUE";
        }
        else
        {
            status = "Active";
        }

        string penalty = "";
        if (LatePenalty > 0)
        {
            penalty = " | Penalty: " + LatePenalty + " PLN";
        }

        return "[Rental #" + Id + "] " + User.Name + " " + User.Surname +
               " -> " + Equipment.Name +
               " | Rented: " + RentedAt.ToString("yyyy-MM-dd") +
               " | Due: " + DueDate.ToString("yyyy-MM-dd") +
               " | " + status + penalty;
    }
}