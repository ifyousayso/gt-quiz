using ITHS.Application.Services;
using ITHS.Application.ViewModels.Questions;
using Microsoft.AspNetCore.Mvc;

namespace ITHS.Webapi.Controllers.V1;

[Route("api/v1/[controller]")]
[ApiExplorerSettings(GroupName = "v1")]
[ApiController]
public class QuestionsController : ControllerBase {
    private readonly QuestionService _questionService;

    public QuestionsController() {
        _questionService = new QuestionService();
    }

    [HttpGet]
    public IActionResult Get(string description) {
        var questions = _questionService.FindQuestionsByDescription(description);
    
        return Ok(questions);
    }

    [HttpPost]
    public IActionResult AddQuestion(QuestionCreateRequest question) {
        _questionService.AddQuestion(question);
    
        return Ok();
    }
}
