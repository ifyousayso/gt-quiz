using ITHS.Domain.Models;

namespace ITHS.Domain.Interfaces.Repositories;

public interface ITheTriviaApiRepository {
    Task<TheTriviaApiProblem> GetTheTriviaApiProblem();
}
