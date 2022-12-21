using ITHS.Application.Services;
using ITHS.Application.ViewModels.Questions;
using Swashbuckle.AspNetCore.Annotations;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Mime;

namespace ITHS.Webapi.Controllers.V1;

[Route("api/v1/[controller]")]
[ApiExplorerSettings(GroupName = "v1")]
[ApiController]
public class QuestionsController : ControllerBase {
    private readonly QuestionService _questionService;

    /// <summary>
    /// Constructor that instantiates a QuestionService
    /// </summary>
    public QuestionsController() {
        _questionService = new QuestionService();
    }

    /// <summary>
    /// Retrieves a random question
    /// </summary>
    [HttpGet]
    [SwaggerResponse((int)HttpStatusCode.OK)]
    [SwaggerResponse((int)HttpStatusCode.NoContent)]
    [ProducesResponseType(typeof(QuestionResponse), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    public async Task<IActionResult> Get() {
        QuestionResponse? question = await _questionService.GetRandomQuestion()!;

        if (question == null) {
            return NoContent();
        }

        return Ok(question);
    }

    /// <summary>
    /// Adds a question
    /// </summary>
    [HttpPost]
    [Consumes(MediaTypeNames.Application.Json)]
    [SwaggerResponse((int)HttpStatusCode.OK)]
    [SwaggerResponse((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(QuestionResponse), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
    public IActionResult Post(QuestionCreateRequest question) {
        QuestionResponse? response = _questionService.AddQuestion(question);
    
        if (response == null) {
            return BadRequest("Invalid category or language");
        }

        return Ok(response);
    }

    /// <summary>
    /// Retrieves a question by id
    /// </summary>
    [HttpGet("{id}", Name = "GetQuestionById")]
    [SwaggerResponse((int)HttpStatusCode.OK)]
    [SwaggerResponse((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(QuestionResponse), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
    public IActionResult Get(Guid id) {
        QuestionResponse? response = _questionService.GetQuestionById(id);
    
        if (response == null) {
            return BadRequest("Invalid id");
        }

        return Ok(response);
    }

    /// <summary>
    /// Updates a specific question
    /// </summary>
    [HttpPut("{id}", Name = "UpdateQuestion")]
    [Consumes(MediaTypeNames.Application.Json)]
    [SwaggerResponse((int)HttpStatusCode.OK)]
    [SwaggerResponse((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(QuestionResponse), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
    public IActionResult Put(Guid id, [FromBody] QuestionUpdateRequest question) {
        QuestionResponse? response = _questionService.UpdateQuestion(id, question);

        if (response == null) {
            return BadRequest("Invalid id, category or language");
        }

        return Ok(response);
    }

    /// <summary>
    /// Deletes a specific question
    /// </summary>
    [HttpDelete("{id}", Name = "DeleteQuestion")]
    [Consumes(MediaTypeNames.Application.Json)]
    [SwaggerResponse((int)HttpStatusCode.OK)]
    [SwaggerResponse((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(QuestionResponse), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
    public IActionResult Delete(Guid id) {
        QuestionResponse? response = _questionService.DeleteQuestion(id);
    
        if (response == null) {
            return BadRequest("Invalid id");
        }

        return Ok(response);
    }
}
