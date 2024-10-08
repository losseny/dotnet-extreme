namespace Domain.Common.Designs.ReservationFactory;

public interface IReservationFactory
{
   public Reservation CreateReservation(ReservationType type, ReservationDate reservationDate, ReservationPeriod period, Guid id);
   public Reservation CreateReservation(ReservationDate reservationDate, ReservationPeriod period, Guid? groupId = default, Guid? bookerId = default);
}
