using ITHS.Application.Services;
using ITHS.Application.ViewModels.Persons;
using Microsoft.AspNetCore.Mvc;

namespace ITHS.Webapi.Controllers.V2
{
    [Route("api/v2/[controller]")]
    [ApiExplorerSettings(GroupName = "v2")]
    [ApiController]
    public class PersonsController : ControllerBase
    {
        IPersonService _personService;
        
        public PersonsController(IPersonService personService)
        {
            _personService = personService;
        }

        [HttpGet]
        public IActionResult Get(string firstName)
        {
            var person = _personService.FindPersonsByFirstName(firstName);
            
            return Ok(person);
        }

        [HttpPost]
        public IActionResult AddPerson(PersonCreateRequest person)
        {
            _personService.AddPerson(person);
            
            return Ok();
        }
    }
}
