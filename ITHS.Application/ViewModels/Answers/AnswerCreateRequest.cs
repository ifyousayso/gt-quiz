using System.ComponentModel.DataAnnotations;

namespace ITHS.Application.ViewModels.Answers;

public class AnswerCreateRequest : AnswerBase {
    [Required]
    public Guid QuestionId { get; set; }

    [Required]
    public bool IsCorrectAnswer { get; set; }
}
