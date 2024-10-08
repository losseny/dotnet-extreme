using Data.Agents.Dto;

namespace Data.Agents;

public class DummyRoomAgent: IAgent
{
    public async Task<T> GetResource<T>(string baseUrl)
    {
        var resource = (T)(object)new RoomAgentDto
        {
            Id = Guid.NewGuid(),
            BuildingPrefix = "D",
            FloorNumber = 1,
            RoomNumber = 1
        };

        return await Task.FromResult(resource);
    }
}
