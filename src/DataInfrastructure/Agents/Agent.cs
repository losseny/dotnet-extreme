using Flurl.Http;

namespace Data.Agents;

public class Agent: IAgent
{
    public async Task<T> GetResource<T>(string baseUrl)
    {
        return await baseUrl.GetJsonAsync<T>();
    }
}
