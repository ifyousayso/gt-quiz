using ITHS.Domain.Entities;
using ITHS.Infrastructure.Persistance;

namespace ITHS.Application.Services;

public interface IOracleService {
    public bool? CheckAnswer(Guid questionId, Guid answerId);
}

public class OracleService : IOracleService {
    public bool? CheckAnswer(Guid questionId, Guid answerId) {
        using (ITHSDatabaseContext context = new ITHSDatabaseContext()) {
            Answer? answer = context
            .Answers
            .Where((answer) => answer.Id == answerId)
            .FirstOrDefault();

            if (answer == null || answer.QuestionId != questionId) {
                return null;
            }

            return answer.IsCorrectAnswer;
        }
    }
}
