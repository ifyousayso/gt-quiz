using ITHS.Domain.Interfaces.Entities;

namespace ITHS.Application.ViewModels.Questions;

public class QuestionResponse : QuestionBase {
    public QuestionResponse(IQuestion question) {
        base.Description = question.Description;
    }
}
