using ITHS.Domain.Constants;

namespace ITHS.Application.ViewModels.Questions;

public class QuestionCreateRequest : QuestionBase {
    public CategoryName CategoryName { get; set; }
    public LanguageName LanguageName { get; set; }
}
