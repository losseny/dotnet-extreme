namespace Domain.ValueObjects;

public record ReservationDate: BaseValueObject
{
    public DateOnly Date { get; private set; }

    protected ReservationDate() { }

    private ReservationDate(DateOnly date)
    {
        if (DateOnly.FromDateTime(DateTime.Now) > date)
        {
            throw new ArgumentException("Reservation date cannot be in the past");
        }
        Date = date;
    }


    public static ReservationDate InstanceFromDate(DateOnly date)
    {
        return new ReservationDate(date);
    }
}
