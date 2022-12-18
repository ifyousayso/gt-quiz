using ITHS.Application.Services;
using ITHS.Application.ViewModels.Answers;
using Microsoft.AspNetCore.Mvc;

namespace ITHS.Webapi.Controllers.V1;

[Route("api/v1/[controller]")]
[ApiExplorerSettings(GroupName = "v1")]
[ApiController]
public class AnswersController : ControllerBase {
    private readonly AnswerService _answerService;

    public AnswersController() {
        _answerService = new AnswerService();
    }

    [HttpGet]
    public IActionResult Get(string description) {
        var answers = _answerService.FindAnswersByDescription(description);
    
        return Ok(answers);
    }

    [HttpPost]
    public IActionResult AddAnswer(AnswerCreateRequest answer) {
        _answerService.AddAnswer(answer);
    
        return Ok();
    }
}
