using Microsoft.AspNetCore.Mvc;
using TheWhiskyRealm.Core.Contracts;
using TheWhiskyRealm.Core.Models.AdminArea.Venue;

namespace TheWhiskyRealm.Areas.Admin.Controllers
{
    public class VenueController : AdminBaseController
    {
        private readonly IVenueService venueService;
        private readonly ICityService cityService;

        public VenueController(IVenueService venueService, ICityService cityService)
        {
            this.venueService = venueService;
            this.cityService = cityService;
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
                ModelState.AddModelError("Name", "There is already a Venue with this name in this city.");
            }

            if (!ModelState.IsValid)
            {
                model.Cities = await cityService.GetAllCitiesAsync();
                return View(model);
            }

            var id = await venueService.AddVenueAsync(model);

            return RedirectToAction("Info", "Venue", new { id });
        }

        [HttpGet]
        public IActionResult Info()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Edit()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Edit(int id)
        {
            return View();
        }
        [HttpGet]
        public IActionResult Delete()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            return View();
        }
    }
}
