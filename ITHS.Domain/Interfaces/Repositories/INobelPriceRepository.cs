
using ITHS.Domain.Models;

namespace ITHS.Domain.Interfaces.Repositories
{
    public interface INobelPrizeRepository
    {
        Task<NobelPrizeWinner> GetNobelPriceWinnerInPhysics(int year);
    }
}
