using ITHS.Domain.Interfaces.Entities;
using ITHS.Application.ViewModels.Answers;
using ITHS.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace ITHS.Application.ViewModels.Questions;

public class QuestionResponse : QuestionBase {
    [Required]
    public Guid Id { get; private set; }

    [Required]
    public List<AnswerUserResponse> Answers { get; private set; }

    public QuestionResponse(IQuestion iQuestion, Category category, Language language, List<AnswerUserResponse> answers) {
        Question question = (Question)iQuestion;

        Id = question.Id;
        base.Description = question.Description;
        base.Category = category.Name;
        base.Language = language.Name;
        Answers = answers;
    }
}
