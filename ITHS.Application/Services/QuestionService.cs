using ITHS.Application.ViewModels.Questions;
using ITHS.Domain.Entities;
using ITHS.Webapi.Persistance;
using Microsoft.EntityFrameworkCore;

namespace ITHS.Application.Services;

public interface IQuestionService {
//    QuestionResponse GetPerson(Guid id);
    List<QuestionResponse> FindQuestionsByDescription(string description);
    void AddQuestion(QuestionCreateRequest question);
    Task<List<QuestionResponse>> FindQuestionsByDescriptionAsync(string description);
}

public class QuestionService : IQuestionService {
//    public PersonService() {
//    }

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

    public List<QuestionResponse> FindQuestionsByDescription(string description) {
        using (ITHSDatabaseContext context = new ITHSDatabaseContext()) {
            var questions = context
                .Questions
                .Where((question) => question.Description.Contains(description))
                .Select((question) => new QuestionResponse(question))
                .ToList();

            return questions;
        }
    }

    public async Task<List<QuestionResponse>> FindQuestionsByDescriptionAsync(string description) {
        using (ITHSDatabaseContext context = new ITHSDatabaseContext()) {
            var questions = await context
                .Questions
                .Where((question) => question.Description.Contains(description))
                .Select((question) => new QuestionResponse(question))
                .ToListAsync();

            return questions;
        }
    }

    public void AddQuestion(QuestionCreateRequest question) {
        using (ITHSDatabaseContext context = new ITHSDatabaseContext()) {
            var category = context.Categories.Where((category) => category.CategoryName == question.CategoryName).FirstOrDefault();
            var language = context.Languages.Where((language) => language.LanguageName == question.LanguageName).FirstOrDefault();

            context.Questions.Add(new Question() {
                Id = Guid.NewGuid(),
                Description = question.Description,
                CategoryId = category.Id,
                Category = category,
                LanguageId = language.Id,
                Language = language
            });

            context.SaveChanges();
        }
    }
}
