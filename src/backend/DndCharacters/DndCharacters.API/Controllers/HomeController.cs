using DndCharacters.Application.Dtos.Home.GetCounters;
using DndCharacters.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DndCharacters.API.Controllers
{
    [ApiController]
    [Route("api/home")]
    public class HomeController(IHomeService homeService) : ControllerBase
    {

        [HttpGet("counters")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(typeof(GetHomeCountersResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> Counters()
        {
            return Ok(await homeService.GetCounters());
        }

    }
}
