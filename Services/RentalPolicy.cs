namespace APBD_TASK2.Services;

public static class RentalPolicy
{
    public const int StudentMaxRentals  = 2;
    public const int EmployeeMaxRentals = 5;
    public const int DefaultRentalDays = 7;

    private const int PenaltyPerDay = 5;
    private const int MaxPenalty    = 100;

    public static int CalculatePenalty(int daysLate)
    {
        if (daysLate <= 0) return 0;
        return Math.Min(daysLate * PenaltyPerDay, MaxPenalty);
    }
}