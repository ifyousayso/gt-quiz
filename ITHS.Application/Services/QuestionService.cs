using ITHS.Application.ViewModels.Questions;
using ITHS.Application.ViewModels.Answers;
using ITHS.Domain.Models;
using ITHS.Infrastructure.Contexts.TheTriviaApi.Repositories;
using ITHS.Domain.Entities;
using ITHS.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;

namespace ITHS.Application.Services;

public interface IQuestionService {
    public Task<QuestionResponse>? GetRandomQuestion();
    public QuestionResponse? GetQuestionById(Guid id);
    public QuestionResponse? AddQuestion(QuestionCreateRequest request);
    public QuestionResponse? UpdateQuestion(Guid id, QuestionUpdateRequest request);
    public QuestionResponse? DeleteQuestion(Guid id);
}

public class QuestionService : IQuestionService {
    private static Question? getDbQuestionById(Guid id, ITHSDatabaseContext context) {
        return context
            .Questions
            .Where((question) => question.Id == id)
            .FirstOrDefault();
    }

    private static Category getDbCategoryById(Guid id, ITHSDatabaseContext context) {
        return context
            .Categories
            .Where((category) => category.Id == id)
            .First();
    }

    private static Language getDbLanguageById(Guid id, ITHSDatabaseContext context) {
        return context
            .Languages
            .Where((language) => language.Id == id)
            .First();
    }

    private static List<AnswerUserResponse> getDbAnswersById(Guid id, ITHSDatabaseContext context) {
        return context
            .Answers
            .Where((answer) => answer.QuestionId == id)
            .OrderBy((answer) => EF.Functions.Random())
            .Select((answer) => new AnswerUserResponse(answer))
            .ToList();
    }

    private static Category? getDbCategoryByName(string name, ITHSDatabaseContext context) {
        return context
            .Categories
            .Where((category) => category.Name == name)
            .FirstOrDefault();
    }

    private static Language? getDbLanguageByName(string name, ITHSDatabaseContext context) {
        return context
            .Languages
            .Where((language) => language.Name == name)
            .FirstOrDefault();
    }

    private static async Task<Question>? GetRandomTheTriviaApiQuestion(ITHSDatabaseContext context) {
        TheTriviaApiRepository theTriviaApiRepository = new TheTriviaApiRepository();

        TheTriviaApiProblem problem = await theTriviaApiRepository.GetTheTriviaApiProblem();

        Guid questionId = Guid.Parse(problem.id!.PadLeft(32, '0'));

        Question? question = getDbQuestionById(questionId, context);
        if (question != null) {
            return question;
        }

        Category? category = getDbCategoryByName(problem.category!, context);

        if (category == null) {
            return null!;
        }

        Language? language = getDbLanguageByName("English", context);

        if (language == null) {
            return null!;
        }

        question = new Question() {
            Id = questionId,
            Description = problem.question!,
            CategoryId = category.Id,
            LanguageId = language.Id,
        };

        context.Questions.Add(question);
        context.SaveChanges();

        context.Answers.Add(new Answer() {
            Id = Guid.NewGuid(),
            Description = problem.correctAnswer!,
            QuestionId = questionId,
            IsCorrectAnswer = true
        });
        foreach (string answerDescription in problem.incorrectAnswers!) {
            context.Answers.Add(new Answer() {
                Id = Guid.NewGuid(),
                Description = answerDescription,
                QuestionId = questionId,
                IsCorrectAnswer = false
            });
        }
        context.SaveChanges();

        return question;
    }

    public async Task<QuestionResponse>? GetRandomQuestion() {
        using (ITHSDatabaseContext context = new ITHSDatabaseContext()) {
            Question? question;

            if (new Random().Next(4) == 0) {
                question = await GetRandomTheTriviaApiQuestion(context)!;
            } else {
                question = await context
                    .Questions
                    .OrderBy((question) => EF.Functions.Random())
                    .FirstOrDefaultAsync();
            }

            if (question == null) {
                return null!;
            }

            return new QuestionResponse(
                question,
                getDbCategoryById(question.CategoryId, context),
                getDbLanguageById(question.LanguageId, context),
                getDbAnswersById(question.Id, context)
            );
        }
    }

    public QuestionResponse? GetQuestionById(Guid id) {
        using (ITHSDatabaseContext context = new ITHSDatabaseContext()) {
            Question? question = getDbQuestionById(id, context);

            if (question == null) {
                return null;
            }

            return new QuestionResponse(
                question,
                getDbCategoryById(question.CategoryId, context),
                getDbLanguageById(question.LanguageId, context),
                getDbAnswersById(question.Id, context)
            );
        }
    }

    public QuestionResponse? AddQuestion(QuestionCreateRequest request) {
        using (ITHSDatabaseContext context = new ITHSDatabaseContext()) {
            Category? category = getDbCategoryByName(request.Category, context);

            if (category == null) {
                return null;
            }

            Language? language = getDbLanguageByName(request.Language, context);

            if (language == null) {
                return null;
            }

            Question question = new Question() {
                Id = Guid.NewGuid(),
                Description = request.Description,
                CategoryId = category.Id,
                LanguageId = language.Id,
            };

            context.Questions.Add(question);
            context.SaveChanges();

            return new QuestionResponse(
                question,
                category,
                language,
                new List<AnswerUserResponse>()
            );
        }
    }

    public QuestionResponse? UpdateQuestion(Guid id, QuestionUpdateRequest request) {
        using (ITHSDatabaseContext context = new ITHSDatabaseContext()) {
            Question? question = getDbQuestionById(id, context);

            if (question == null) {
                return null;
            }

            Category? category = getDbCategoryByName(request.Category, context);

            if (category == null) {
                return null;
            }

            Language? language = getDbLanguageByName(request.Language, context);

            if (language == null) {
                return null;
            }

            question.Description = request.Description;
            question.CategoryId = category.Id;
            question.LanguageId = language.Id;

            context.SaveChanges();

            return new QuestionResponse(
                question,
                category,
                language,
                getDbAnswersById(question.Id, context)
            );
        }
    }

    public QuestionResponse? DeleteQuestion(Guid id) {
        using (ITHSDatabaseContext context = new ITHSDatabaseContext()) {
            Question? question = getDbQuestionById(id, context);

            if (question == null) {
                return null;
            }

            context.Questions.Remove(question);
            context.SaveChanges();

            return new QuestionResponse(
                question,
                getDbCategoryById(question.CategoryId, context),
                getDbLanguageById(question.LanguageId, context),
                getDbAnswersById(question.Id, context)
            );
        }
    }
}
