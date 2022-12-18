using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;
using System.ComponentModel.DataAnnotations;
using System.Net.Mime;
using ITHS.Domain.Interfaces.Repositories;

namespace ITHS.Webapi.Controllers.V1
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "v1")]
    public class NobelPrizeController : ControllerBase
    {
        private readonly INobelPrizeRepository _nobelPriceRepository;
        public NobelPrizeController(INobelPrizeRepository nobelPriceRepository)
        {
            _nobelPriceRepository = nobelPriceRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get(int year)
        {
            var nobelPriceWinner = await _nobelPriceRepository.GetNobelPriceWinnerInPhysics(year);

            return Ok(nobelPriceWinner);
        }

    }
}
