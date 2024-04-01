using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using TheWhiskyRealm.Core.Contracts;

namespace TheWhiskyRealm.Controllers
{
    public class EventController : BaseController
    {
        private readonly IEventService eventService;

        public EventController(IEventService eventService)
        {
            this.eventService = eventService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var model = await eventService.GetAllEventsAsync();

            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> PastEvents()
        {
            var model = await eventService.GetAllPastEventsAsync();

            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var model = await eventService.GetEventAsync(id);
            if(model == null)
            {
                return NotFound();
            }

            return View(model);
        }
    }
}
