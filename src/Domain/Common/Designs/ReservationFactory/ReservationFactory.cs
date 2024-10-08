namespace Domain.Common.Designs.ReservationFactory;

public class ReservationFactory: IReservationFactory
{
    public Reservation CreateReservation(ReservationType type, ReservationDate reservationDate, ReservationPeriod period, Guid id)
    {
        return type switch
        {
            ReservationType.Study => new StudyReservation(reservationDate, period, id),
            ReservationType.Free => new FreeReservation(reservationDate, period, id),
            _ => throw new ArgumentException("Invalid reservation type")
        };
    }

    public Reservation CreateReservation(ReservationDate reservationDate, ReservationPeriod period, Guid? groupId = default,
        Guid? bookerId = default)
    {
        if (groupId is not null && bookerId is null)
        {
            return new StudyReservation(reservationDate, period, groupId.Value);
        }

        if (bookerId is not null && groupId is null)
        {
            return new FreeReservation(reservationDate, period, bookerId.Value);
        }
        throw new ArgumentException("Invalid reservation type");
    }
}
