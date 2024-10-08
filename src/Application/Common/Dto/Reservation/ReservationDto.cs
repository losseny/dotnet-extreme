using Application.Common.Dto.Common;
using Application.Common.Mappings;

namespace Application.Common.Dto.Reservation;

public record ReservationDto: IMapToFrom<Domain.Entities.Reservation>
{
    public Guid Id { get; private set; }
    public required ReservationDateDto ReservationDate { get; set; }
    public required ReservationPeriodDto Period { get; set; }
    public Guid StudyGroupId { get; private set; }
    public Guid BookerId { get; private set; }
    public ReservationDto() { }
}
