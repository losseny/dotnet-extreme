using System.Text.Json.Serialization;
using Application.Common.Mappings;
using Domain.ValueObjects;

namespace Application.Common.Dto.Common;

public record ReservationDateDto: IMapToFrom<ReservationDate>
{
    [JsonPropertyName("date")]
    public required DateOnly Date { get; set; }

    public ReservationDateDto() { }
}
