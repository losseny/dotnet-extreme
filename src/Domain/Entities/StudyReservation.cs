using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class StudyReservation: Reservation
{
    [Column("study_group_id")]
    public Guid StudyGroupId { get; private set; }

    protected StudyReservation() { }

    public StudyReservation(ReservationDate reservationDate, ReservationPeriod period, Guid studyGroupId)
        : base(reservationDate, period)
    {
        StudyGroupId = studyGroupId;
    }
}
