using ITHS.Application.Services;
using ITHS.Application.ViewModels.Persons;
using Microsoft.AspNetCore.Mvc;

namespace ITHS.Webapi.Controllers.V3
{
    [Route("api/v3/[controller]")]
    [ApiExplorerSettings(GroupName = "v3")]
    [ApiController]
    public class PersonsController : ControllerBase
    {
        IPersonService _personService;

        public PersonsController(IPersonService personService)
        {
            _personService = personService;
        }
        
        [HttpGet]
        public async Task<ActionResult<List<PersonResponse>>> Get(string firstName)
        {
            var person =  await _personService.FindPersonsByFirstNameAsync(firstName);
            
            return Ok(person);
        }

    }
}
