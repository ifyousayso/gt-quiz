using ITHS.Domain.Interfaces.Entities;
using ITHS.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace ITHS.Application.ViewModels.Answers;

public class AnswerResponse : AnswerBase {
    [Required]
    public Guid QuestionId { get; private set; }

    [Required]
    public bool IsCorrectAnswer { get; private set; }

    public AnswerResponse(IAnswer iAnswer) {
        Answer answer = (Answer)iAnswer;

        base.Description = answer.Description;
        QuestionId = answer.QuestionId;
        IsCorrectAnswer = answer.IsCorrectAnswer;
    }
}
