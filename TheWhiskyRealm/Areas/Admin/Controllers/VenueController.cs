using Microsoft.AspNetCore.Mvc;
using TheWhiskyRealm.Core.Contracts;
using TheWhiskyRealm.Core.Models.AdminArea.Venue;
using static TheWhiskyRealm.Core.Constants.ControllerConstants;

namespace TheWhiskyRealm.Areas.Admin.Controllers
{
    public class VenueController : AdminBaseController
    {
        private readonly IVenueService venueService;
        private readonly ICityService cityService;
        private readonly IEventService eventService;

        public VenueController(IVenueService venueService, ICityService cityService,IEventService eventService)
        {
            this.venueService = venueService;
            this.cityService = cityService;
            this.eventService = eventService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int currentPage = 1, int pageSize = 10)
        {
            var totalVenues = await venueService.GetTotalVenuesAsync();

            var venues = await venueService.GetVenuesAsync(currentPage, pageSize);

            var model = new VenueIndexViewModel
            {
                Venues = venues,
                CurrentPage = currentPage,
                TotalPages = (int)Math.Ceiling(totalVenues / (double)pageSize)
            };

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var model = new VenueFormViewModel();
            model.Cities = await cityService.GetAllCitiesAsync();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(VenueFormViewModel model)
        {
            if(model == null)
            {
                return BadRequest();
            }

            if (await venueService.VenueExistByNameAsync(model.Name,model.CityId))
            {
                ModelState.AddModelError(nameof(model.Name), VenueWithThisNameInThisCityMessage);
            }

            if (!ModelState.IsValid)
            {
                model.Cities = await cityService.GetAllCitiesAsync();
                return View(model);
            }

            var id = await venueService.AddVenueAsync(model);

            return RedirectToAction(nameof(Info), "Venue", new { id });
        }

        public async Task<IActionResult> Info(int id)
        {
            var venue = await venueService.GetVenueByIdAsync(id);
            if (venue == null)
            {
                return NotFound();
            }

            var pendingEvents = await eventService.GetAllEventsInVenueAsync(id);
            var pastEvents = await eventService.GetAllPastEventsInVenueAsync(id);

            var model = new VenueInfoViewModel
            {
                VenueName = venue.Name,
                PastEvents = pastEvents,
                PendingEvents = pendingEvents
            };

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var model = await venueService.GetVenueByIdAsync(id);

            if (model == null)
            {
                return NotFound();
            }

            model.Cities = await cityService.GetAllCitiesAsync();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(VenueFormViewModel model)
        {

            if (model == null)
            {
                return BadRequest();
            }

            var venue = await venueService.GetVenueByIdAsync(model.Id);

            if (model == null)
            {
                return NotFound();
            }

            if (await cityService.CityExistAsync(model.CityId) == false)
            {
                return NotFound();
            }

            if (await venueService.VenueExistByNameAsync(model.Name, model.CityId,model.Id))
            {
                ModelState.AddModelError(nameof(model.Name), VenueWithThisNameInThisCityMessage);
            }

            if (!ModelState.IsValid)
            {
                model.Cities = await cityService.GetAllCitiesAsync();
                return View(model);
            }

            await venueService.EditVenueAsync(model);

            return RedirectToAction(nameof(Info), "Venue", new { model.Id });
        }
    }
}
