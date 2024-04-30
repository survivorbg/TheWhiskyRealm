using Microsoft.AspNetCore.Mvc;
using TheWhiskyRealm.Core.Contracts;

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
    }
}
