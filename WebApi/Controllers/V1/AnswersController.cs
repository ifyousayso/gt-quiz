using ITHS.Application.Services;
using ITHS.Application.ViewModels.Answers;
using Swashbuckle.AspNetCore.Annotations;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Mime;

namespace ITHS.Webapi.Controllers.V1;

/// <summary>
/// Controller class for answers
/// </summary>
[Route("api/v1/[controller]")]
[ApiExplorerSettings(GroupName = "v1")]
[ApiController]
public class AnswersController : ControllerBase {
    private readonly AnswerService _answerService;

    /// <summary>
    /// Constructor that instantiates an AnswerService
    /// </summary>
    public AnswersController() {
        _answerService = new AnswerService();
    }

    /// <summary>
    /// Adds an answer
    /// </summary>
    [HttpPost]
    [Consumes(MediaTypeNames.Application.Json)]
    [SwaggerResponse((int)HttpStatusCode.OK)]
    [SwaggerResponse((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(AnswerResponse), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
    public IActionResult Post(AnswerCreateRequest answer) {
        AnswerResponse? response = _answerService.AddAnswer(answer);
    
        if (response == null) {
            return BadRequest("Invalid question id");
        }

        return Ok(response);
    }

    /// <summary>
    /// Retrieves an answer by id
    /// </summary>
    [HttpGet("{id}", Name = "GetAnswerById")]
    [SwaggerResponse((int)HttpStatusCode.OK)]
    [SwaggerResponse((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(AnswerResponse), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
    public IActionResult Get(Guid id) {
        AnswerResponse? response = _answerService.GetAnswerById(id);
    
        if (response == null) {
            return BadRequest("Invalid id");
        }

        return Ok(response);
    }

    /// <summary>
    /// Updates a specific answer
    /// </summary>
    [HttpPut("{id}", Name = "UpdateAnswer")]
    [Consumes(MediaTypeNames.Application.Json)]
    [SwaggerResponse((int)HttpStatusCode.OK)]
    [SwaggerResponse((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(AnswerResponse), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
    public IActionResult Put(Guid id, [FromBody] AnswerUpdateRequest answer) {
        AnswerResponse? response = _answerService.UpdateAnswer(id, answer);

        if (response == null) {
            return BadRequest("Invalid id or question id");
        }

        return Ok(response);
    }

    /// <summary>
    /// Deletes a specific answer
    /// </summary>
    [HttpDelete("{id}", Name = "DeleteAnswer")]
    [Consumes(MediaTypeNames.Application.Json)]
    [SwaggerResponse((int)HttpStatusCode.OK)]
    [SwaggerResponse((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(AnswerResponse), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
    public IActionResult Delete(Guid id) {
        AnswerResponse? response = _answerService.DeleteAnswer(id);
    
        if (response == null) {
            return BadRequest("Invalid id");
        }

        return Ok(response);
    }
}
