using ITHS.Domain.Interfaces.Entities;
using ITHS.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace ITHS.Application.ViewModels.Answers;

public class AnswerUserResponse : AnswerBase {
    [Required]
    public Guid Id { get; private set; }

    public AnswerUserResponse(IAnswer iAnswer) {
        Answer answer = (Answer)iAnswer;

        Id = answer.Id;
        base.Description = answer.Description;
    }
}
