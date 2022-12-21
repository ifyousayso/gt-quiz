using ITHS.Application.ViewModels.Auth;
using ITHS.Infrastructure.Configurations.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Net;

namespace ITHS.Webapi.Controllers.V1;

/// <summary>
/// Controller class for authentication
/// </summary>
[Route("api/v1/[controller]")]
[ApiExplorerSettings(GroupName = "v1")]
[ApiController]
public class AuthenticationController : ControllerBase {
    /// <summary>
    /// Attempts to log in the user
    /// </summary>
    [HttpPost]
    [SwaggerResponse((int)HttpStatusCode.OK)]
    [SwaggerResponse((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public IActionResult Login(AuthenticationRequest authenticationRequest) {
        try {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            string serverHostUrl = $"{Request.Scheme}://{Request.Host.Host}:{Request.Host.Port}";
        
            string token = Authentication.CreateJWTBearerToken(serverHostUrl);
        
            return Ok(token);
        } catch (Exception e) {
            return BadRequest(e.Message);
        }
    }

    /// <summary>
    /// Check if the user is authorized
    /// </summary>
    [HttpGet, Authorize]
    [SwaggerResponse((int)HttpStatusCode.Unauthorized, "Unauthorized")]
    [SwaggerResponse((int)HttpStatusCode.OK, "Login successful")]
    public IActionResult CheckAccess() {
        return Ok(HttpContext.User.Claims);
    }
}
