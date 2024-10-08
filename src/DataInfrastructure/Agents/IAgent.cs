namespace Data.Agents;

public interface IAgent
{
    Task<T> GetResource<T>(string baseUrl);
}
