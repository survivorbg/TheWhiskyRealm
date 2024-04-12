using Microsoft.AspNetCore.Mvc;
using TheWhiskyRealm.Core.Contracts;
using TheWhiskyRealm.Core.Models.AdminArea.City;
using TheWhiskyRealm.Core.Models.AdminArea.Region;
using TheWhiskyRealm.Core.Services;

namespace TheWhiskyRealm.Areas.Admin.Controllers
{
    public class CityController : AdminBaseController
    {
        private readonly ICountryService countryService;
        private readonly ICityService cityService;

        public CityController(ICountryService countryService, ICityService cityService)
        {
            this.countryService = countryService;
            this.cityService = cityService;
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
    }
}
