using System.Text.Json.Serialization;
using Application.Common.Mappings;
using Domain.ValueObjects;

namespace Application.Common.Dto.Common;

public record ReservationPeriodDto: IMapToFrom<ReservationPeriod>
{
    [JsonPropertyName("start")]
    public TimeOnly Start { get; set; }
    [JsonPropertyName("end")]
    public TimeOnly End { get; set; }

    public ReservationPeriodDto() { }
}
