using Microsoft.AspNetCore.Mvc;
using TheWhiskyRealm.Core.Contracts;
using TheWhiskyRealm.Core.Models.AdminArea.Country;

namespace TheWhiskyRealm.Areas.Admin.Controllers;

public class CountryController : AdminBaseController
{
    private readonly ICountryService countryService;
    private readonly IRegionService regionService;

    public CountryController(ICountryService countryService, IRegionService regionService)
    {
        this.countryService = countryService;
        this.regionService = regionService;
    }

    [HttpGet]
    public async Task<IActionResult> Index(int currentPage = 1, int pageSize = 10)
    {
        var totalCountries = await countryService.GetTotalCountriesAsync();
        var countries = await countryService.GetAllCountriesAsync(currentPage, pageSize);

        var model = new CountryIndexViewModel
        {
            Countries = countries,
            CurrentPage = currentPage,
            TotalPages = (int)Math.Ceiling(totalCountries / (double)pageSize)
        };

        return View(model);
    }


    [HttpPost]
    public async Task<IActionResult> Add(string name)
    {
        if (await countryService.CountryWithNameExistsAsync(name))
        {
            return BadRequest();
        }

        if (ModelState.IsValid)
        {
            await countryService.AddCountryAsync(name);
        }

        return RedirectToAction(nameof(Index)); //TODO change everything to nameof
    }

    public async Task<IActionResult> Edit(int id)
    {
        var model = await countryService.GetByIdAsync(id);

        if (model == null)
        {
            return NotFound();
        }

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(CountryViewModel model)
    {

        if (model == null) //TODO Check for null on every post
        {
            return BadRequest("Invalid request");
        }
        var country = await countryService.GetByIdAsync(model.Id);

        if (country == null)
        {
            return NotFound();
        }

        if (await countryService.CountryWithNameExistsAsync(model.Name,model.Id))
        {
            ModelState.AddModelError("Name", "There is already a country with that name.");
            return View(model);
        }

        if (!ModelState.IsValid)
        {
            return View(model);
        }

        await countryService.EditAsync(model);

        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Info(int id)
    {
        var country = await countryService.GetByIdAsync(id);

        if (country == null)
        {
            return NotFound();
        }

        var model = new CountryInfoViewModel()
        {
            Id = id,
            Name = country.Name,
            Regions = await regionService.GetAllRegionsByCountryIdAsync(id)
        };

        return View(model);
    }

}
