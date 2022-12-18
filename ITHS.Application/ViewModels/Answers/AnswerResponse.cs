using ITHS.Domain.Interfaces.Entities;

namespace ITHS.Application.ViewModels.Answers;

public class AnswerResponse : AnswerBase {
    public AnswerResponse(IAnswer answer) {
        base.Description = answer.Description;
    }
}
