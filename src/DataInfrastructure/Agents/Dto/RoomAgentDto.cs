namespace Data.Agents.Dto;

public record RoomAgentDto
{
    public Guid Id { get; init; }
    public string BuildingPrefix { get; init; }
    public int FloorNumber { get; init; }
    public int RoomNumber { get; init; }
}
