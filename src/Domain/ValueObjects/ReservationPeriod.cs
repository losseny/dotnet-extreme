namespace Domain.ValueObjects;

public record ReservationPeriod: BaseValueObject
{

    public TimeOnly Start { get; private set; }
    public TimeOnly End { get; private set; }
    protected ReservationPeriod()
    {
    }

    private ReservationPeriod(TimeOnly start, TimeOnly end)
    {
        Start = start;
        End = end;
    }

    public static ReservationPeriod InstanceFromTime(DateTime start, DateTime end)
    {
        if (start > end && start.Date == end.Date)
        {
            throw new ArgumentException("Start time cannot be after end time");
        }
        return new ReservationPeriod(TimeOnly.FromDateTime(start), TimeOnly.FromDateTime(end));
    }

    public static ReservationPeriod InstanceFromTime(TimeOnly start, TimeOnly end)
    {
        if (start > end)
        {
            throw new ArgumentException("Start time cannot be after end time");
        }
        return new ReservationPeriod(start, end);
    }
}
