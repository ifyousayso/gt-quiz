using ITHS.Application.ViewModels.Answers;
using ITHS.Domain.Entities;
using ITHS.Webapi.Persistance;
using Microsoft.EntityFrameworkCore;

namespace ITHS.Application.Services;

public interface IAnswerService {
//    QuestionResponse GetPerson(Guid id);
    List<AnswerResponse> FindAnswersByDescription(string description);
    void AddAnswer(AnswerCreateRequest answer);
    Task<List<AnswerResponse>> FindAnswersByDescriptionAsync(string description);
}

public class AnswerService : IAnswerService {
/*
    public QuestionResponse GetPerson(Guid id) {
        using (ITHSDatabaseContext context = new ITHSDatabaseContext()) {
            var person = context
                .Persons
                .Where((p) => p.Id == id)
                .FirstOrDefault();

            return new QuestionResponse(person);
        }
    }
*/

    public List<AnswerResponse> FindAnswersByDescription(string description) {
        using (ITHSDatabaseContext context = new ITHSDatabaseContext()) {
            var answers = context
                .Answers
                .Where((answer) => answer.Description.Contains(description))
                .Select((answer) => new AnswerResponse(answer))
                .ToList();

            return answers;
        }
    }

    public async Task<List<AnswerResponse>> FindAnswersByDescriptionAsync(string description) {
        using (ITHSDatabaseContext context = new ITHSDatabaseContext()) {
            var answers = await context
                .Answers
                .Where((answer) => answer.Description.Contains(description))
                .Select((answer) => new AnswerResponse(answer))
                .ToListAsync();

            return answers;
        }
    }

    public void AddAnswer(AnswerCreateRequest answer) {
        using (ITHSDatabaseContext context = new ITHSDatabaseContext()) {
            context.Answers.Add(new Answer() {
                Id = Guid.NewGuid(),
                Description = answer.Description,
                QuestionId = answer.QuestionId,
                IsCorrectAnswer = answer.IsCorrectAnswer
            });

            context.SaveChanges();
        }
    }
}
