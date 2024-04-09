using Microsoft.AspNetCore.Mvc;
using TheWhiskyRealm.Core.Contracts;
using TheWhiskyRealm.Core.Models.AdminArea;
using TheWhiskyRealm.Core.Models.AdminArea.Country;
using TheWhiskyRealm.Core.Services;

namespace TheWhiskyRealm.Areas.Admin.Controllers;

public class CountryController : AdminBaseController
{
    private readonly ICountryService countryService;

    public CountryController(ICountryService countryService)
    {
        this.countryService = countryService;
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
        if(await countryService.CountryWithNameExistsAsync(name))
        {
            return BadRequest();
        }

        if(ModelState.IsValid)
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

        if (model == null)
        {
            return BadRequest("Invalid request");
        }

        if (await countryService.CountryWithNameExistsAsync(model.Name))
        {
            ModelState.AddModelError("Name", "There is already a country with that name.");
            return View(model);
        }

        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var country = await countryService.GetByIdAsync(model.Id);

        if (country == null)
        {
            return NotFound();
        }

        await countryService.EditAsync(model);

        return RedirectToAction(nameof(Index));
    }
}
