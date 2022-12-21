using ITHS.Application.ViewModels.Answers;
using ITHS.Domain.Entities;
using ITHS.Infrastructure.Persistance;

namespace ITHS.Application.Services;

public interface IAnswerService {
    public AnswerResponse? GetAnswerById(Guid id);
    public AnswerResponse? AddAnswer(AnswerCreateRequest request);
    public AnswerResponse? UpdateAnswer(Guid id, AnswerUpdateRequest request);
    public AnswerResponse? DeleteAnswer(Guid id);
}

public class AnswerService : IAnswerService {
    private static Answer? getDbAnswerById(Guid id, ITHSDatabaseContext context) {
        return context
            .Answers
            .Where((answer) => answer.Id == id)
            .FirstOrDefault();
    }

    private static Question? getDbQuestionById(Guid id, ITHSDatabaseContext context) {
        return context
            .Questions
            .Where((question) => question.Id == id)
            .FirstOrDefault();
    }

    public AnswerResponse? GetAnswerById(Guid id) {
        using (ITHSDatabaseContext context = new ITHSDatabaseContext()) {
            Answer? answer = context
                .Answers
                .Where((answer) => answer.Id.Equals(id))
                .FirstOrDefault();

            if (answer == null) {
                return null;
            }

            return new AnswerResponse(
                answer
            );
        }
    }

    public AnswerResponse? AddAnswer(AnswerCreateRequest request) {
        using (ITHSDatabaseContext context = new ITHSDatabaseContext()) {
            Question? question = getDbQuestionById(request.QuestionId, context);

            if (question == null) {
                return null;
            }

            Answer answer = new Answer() {
                Id = Guid.NewGuid(),
                Description = request.Description,
                QuestionId = request.QuestionId,
                IsCorrectAnswer = request.IsCorrectAnswer
            };

            context.Answers.Add(answer);
            context.SaveChanges();

            return new AnswerResponse(
                answer
            );
        }
    }

    public AnswerResponse? UpdateAnswer(Guid id, AnswerUpdateRequest request) {
        using (ITHSDatabaseContext context = new ITHSDatabaseContext()) {
            Answer? answer = getDbAnswerById(id, context);

            if (answer == null) {
                return null;
            }

            Question? question = getDbQuestionById(request.QuestionId, context);

            if (question == null) {
                return null;
            }

            answer.Description = request.Description;
            answer.QuestionId = request.QuestionId;
            answer.IsCorrectAnswer = request.IsCorrectAnswer;

            context.SaveChanges();

            return new AnswerResponse(
                answer
            );
        }
    }

    public AnswerResponse? DeleteAnswer(Guid id) {
        using (ITHSDatabaseContext context = new ITHSDatabaseContext()) {
            Answer? answer = getDbAnswerById(id, context);

            if (answer == null) {
                return null;
            }

            context.Answers.Remove(answer);
            context.SaveChanges();

            return new AnswerResponse(
                answer
            );
        }
    }
}
