using System.Text.Json.Serialization;
using Application.Common.Dto.Common;

namespace Application.Common.Dto.Reservation;

public record CreateReservationDto
{
    [JsonPropertyName("reservationDate")]
    public ReservationDateDto ReservationDate { get; set; } = null!;
    [JsonPropertyName("period")]
    public ReservationPeriodDto Period { get; set; } = null!;
    [JsonPropertyName("studyGroupId")]
    public Guid? StudyGroupId { get; set; }
    [JsonPropertyName("bookerId")]
    public Guid? BookerId { get; set; }
}
