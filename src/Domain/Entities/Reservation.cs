namespace Domain.Entities;

public class Reservation : BaseEntity
{
    public ReservationDate ReservationDate { get; private set; }
    public ReservationPeriod Period { get; private set; }

    public Guid RoomId { get; private init; }
    public Room Room { get; private set; }

    protected Reservation() { }

    protected Reservation(ReservationDate reservationDate, ReservationPeriod period)
    {
        ReservationDate = reservationDate;
        Period = period;
    }

    public override string ToString()
    {
        return $"Date:{ReservationDate.Date} Time: {Period.Start} - {Period.End}";
    }
}
