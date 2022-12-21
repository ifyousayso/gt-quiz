using ITHS.Application.Services;
using Swashbuckle.AspNetCore.Annotations;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Mime;

namespace ITHS.Webapi.Controllers.V1;

/// <summary>
/// Controller class for oracle
/// </summary>
[Route("api/v1/[controller]")]
[ApiExplorerSettings(GroupName = "v1")]
[ApiController]
public class OracleController : ControllerBase {
    private readonly OracleService _oracleService;

    /// <summary>
    /// Constructor that instantiates an OracleService
    /// </summary>
    public OracleController() {
        _oracleService = new OracleService();
    }

    /// <summary>
    /// Compares a question to an answer by ids
    /// </summary>
    [HttpGet("{questionId}/{answerId}", Name = "CheckAnswerByIds")]
    [SwaggerResponse((int)HttpStatusCode.OK)]
    [SwaggerResponse((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
    public IActionResult Get(Guid questionId, Guid answerId) {
        bool? response = _oracleService.CheckAnswer(questionId, answerId);
    
        if (response == null) {
            return BadRequest("Invalid question id and/or answer id");
        }

        return Ok(response);
    }
}
