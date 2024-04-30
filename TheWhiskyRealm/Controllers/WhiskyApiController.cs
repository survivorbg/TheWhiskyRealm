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
        private readonly IDistilleryService distilleryService;

        public WhiskyApiController(IWhiskyService whiskyService,
            IDistilleryService distilleryService)
        {
            this.whiskyService = whiskyService;
            this.distilleryService = distilleryService;
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

        [HttpGet("distilleries")]
        public async Task<IActionResult> GetAllDistilleries()
        {
            var distilleries = await distilleryService.GetAllDistilleriesForApi();


            return Ok(distilleries);
        }
    }
}
