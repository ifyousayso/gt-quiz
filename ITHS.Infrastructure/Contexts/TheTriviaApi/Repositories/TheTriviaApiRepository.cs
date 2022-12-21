using ITHS.Domain.Interfaces.Repositories;
using ITHS.Domain.Models;
using System.Text.Json;

namespace ITHS.Infrastructure.Contexts.TheTriviaApi.Repositories;

public class TheTriviaApiRepository : ITheTriviaApiRepository {
    public async Task<TheTriviaApiProblem> GetTheTriviaApiProblem() {
        try {
            string uri = "https://the-trivia-api.com/api/questions?categories=music,history,film_and_tv&limit=1";

            using HttpClient client = new HttpClient();

            HttpResponseMessage response = await client.GetAsync(uri);

            Stream stream =  await response.Content.ReadAsStreamAsync();

            List<TheTriviaApiProblem>? problems = await JsonSerializer.DeserializeAsync<List<TheTriviaApiProblem>>(stream);
        
            return problems![0];
        } catch (Exception exception) {
            throw new Exception(exception.Message);
        }
    }
}
