using Data.Agents.Dto;

namespace Data.Agents;

public class DummyStudyGroupAgent: IAgent
{
    public async Task<T> GetResource<T>(string baseUrl)
    {
        var id = Guid.Parse("8c38706d-3271-44a8-99c7-2f2a7a50f613");
        var value = (T)(object) new StudyGroupAgentDto {Id = id};
        return await Task.FromResult(value);
    }
}
