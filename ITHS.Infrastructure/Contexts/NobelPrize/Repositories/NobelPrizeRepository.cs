using ITHS.Domain.Interfaces.Repositories;
using ITHS.Domain.Models;
using System.Text.Json;

namespace ITHS.Infrastructure.Contexts.NobelPrize.Repositories
{
    public class NobelPrizeRepository : INobelPrizeRepository
    {
        public async Task<NobelPrizeWinner> GetNobelPriceWinnerInPhysics(int year)
        {
            try
            {
                var uri = $"http://api.nobelprize.org/2.0/nobelPrize/phy/{year}";

                using var client = new HttpClient();

                var response = await client.GetAsync(uri);

                var stream =  await response.Content.ReadAsStreamAsync();

                var nobelPrizeWinner = await JsonSerializer.DeserializeAsync<List<NobelPrizeWinner>>(stream);
                
                return nobelPrizeWinner[0];

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
