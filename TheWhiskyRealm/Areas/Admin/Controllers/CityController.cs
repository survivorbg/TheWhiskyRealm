using Microsoft.AspNetCore.Mvc;
using System.IO.Compression;
using TheWhiskyRealm.Core.Contracts;
using TheWhiskyRealm.Core.Models.AdminArea.City;
using TheWhiskyRealm.Core.Models.AdminArea.Country;
using TheWhiskyRealm.Core.Models.AdminArea.Region;
using TheWhiskyRealm.Core.Services;

namespace TheWhiskyRealm.Areas.Admin.Controllers
{
    public class CityController : AdminBaseController
    {
        private readonly ICountryService countryService;
        private readonly ICityService cityService;
        private readonly IVenueService venueService;

        public CityController(ICountryService countryService,
            ICityService cityService,
            IVenueService venueService)
        {
            this.countryService = countryService;
            this.cityService = cityService;
            this.venueService = venueService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int currentPage = 1, int pageSize = 10)
        {
            int totalCities = await cityService.GetTotalCitiesAsync();
            IEnumerable<CityViewModel> cities = await cityService.GetAllCitiesAsync(currentPage, pageSize);

            var model = new CityIndexViewModel
            {
                Cities = cities,
                CurrentPage = currentPage,
                TotalPages = (int)Math.Ceiling(totalCities / (double)pageSize)
            };

            return View(model);
        }

        public async Task<IActionResult> Add(int? countryId)
        {
            var model = new CityFormViewModel();

            if (countryId != null && await countryService.CountryExistsAsync((int)countryId) == false)
            {
                return BadRequest();
            }

            model.CountryId = countryId;
            model.Countries = await countryService.GetAllCountriesAsync();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(CityFormViewModel model)
        {
            if (model.CountryId == null || await countryService.CountryExistsAsync((int)model.CountryId) == false)
            {
                model.Countries = await countryService.GetAllCountriesAsync();
                return View(model);
            }

            if (await cityService.CityWithThisNameAndCountryExistsAsync(model.Name, (int)model.CountryId))
            {
                ModelState.AddModelError("Name", "There is already a city with this name in this country.");
            }

            if (!ModelState.IsValid)
            {
                model.Countries = await countryService.GetAllCountriesAsync();
                return View(model);
            }

            var id = await cityService.AddCityAsync(model.Name, (int)model.CountryId,model.Zip);

            return RedirectToAction("Info", "City", new { id });
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var model = await cityService.GetCityByIdAsync(id);

            if (model == null)
            {
                return NotFound();
            }

            if (model.CountryId != null && await countryService.CountryExistsAsync((int)model.CountryId) == false)
            {
                return NotFound();
            }

            model.Countries = await countryService.GetAllCountriesAsync();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CityFormViewModel model)
        {

            if (model == null || model.CountryId == null) //TODO Check for null on every post
            {
                return BadRequest("Invalid request");
            }

            var city = await cityService.GetCityByIdAsync(model.Id);

            if (model == null)
            {
                return NotFound();
            }

            if (await countryService.CountryExistsAsync((int)model.CountryId) == false)
            {
                return NotFound();
            }

            if (await cityService.CityWithThisNameAndCountryExistsAsync(model.Name, (int)model.CountryId, model.Id))
            {
                ModelState.AddModelError("Name", "There is already a city with this name in this country.");
            }

            if (!ModelState.IsValid)
            {
                model.Countries = await countryService.GetAllCountriesAsync();
                return View(model);
            }

            await cityService.EditCityAsync(model);

            return RedirectToAction("Info", "City", new { id = model.Id });
        }

        [HttpGet]
        public async Task<IActionResult> Info(int id)
        {
            var city = await cityService.GetCityByIdAsync(id);

            if (city == null)
            {
                return NotFound();
            }

            var model = new CityInfoViewModel()
            {
                Id = id,
                Name = city.Name,
                Zip = city.Zip,
                Venues = await venueService.GetVenuesByCityAsync(id)
            };

            return View(model);
        }
    }
}
