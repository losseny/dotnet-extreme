using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class FreeReservation: Reservation
{
    [Column("booker_id")]
    public Guid BookerId { get; private set; }

    protected FreeReservation() { }

    public FreeReservation(ReservationDate reservationDate, ReservationPeriod period, Guid bookerId)
        : base(reservationDate, period)
    {
        BookerId = bookerId;
    }
}
