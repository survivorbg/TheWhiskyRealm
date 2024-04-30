using Microsoft.AspNetCore.Mvc;
using TheWhiskyRealm.Core.Contracts;
using TheWhiskyRealm.Core.Models.Whisky.WhiskyApi;

namespace TheWhiskyRealm.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WhiskyApiController : ControllerBase
    {
        private readonly IWhiskyService whiskyService;

        public WhiskyApiController(IWhiskyService whiskyService)
        {
            this.whiskyService = whiskyService;
        }

        [HttpGet("all")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllWhiskies()
        {
            var whiskies = await whiskyService.GetAllWhiskiesAsync();
            return Ok(whiskies);
        }


        [HttpGet("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetWhiskyDetails(int id)
        {
            var whisky = await whiskyService.GetWhiskyApiModelByIdAsync(id);

            if (whisky == null)
            {
                return NotFound();
            }

            return Ok(whisky);
        }
    }
}
