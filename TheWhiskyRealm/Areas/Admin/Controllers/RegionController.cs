using Microsoft.AspNetCore.Mvc;
using TheWhiskyRealm.Core.Contracts;
using TheWhiskyRealm.Core.Models.AdminArea.Country;
using TheWhiskyRealm.Core.Models.AdminArea.Region;

namespace TheWhiskyRealm.Areas.Admin.Controllers;

public class RegionController : AdminBaseController
{
    private readonly IRegionService regionService;
    private readonly ICountryService countryService;

    public RegionController(IRegionService regionService, ICountryService countryService)
    {
        this.regionService = regionService;
        this.countryService = countryService;
    }

    public async Task<IActionResult> Add(int? countryId)
    {
        var model = new AddRegionViewModel();

        if(countryId != null && await countryService.CountryExistsAsync((int)countryId) == false)
        {
            return BadRequest();
        }

        model.CountryId = countryId;
        model.Countries = await countryService.GetAllCountriesAsync();
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Add(AddRegionViewModel model)
    {
        if(model.CountryId == null || await countryService.CountryExistsAsync((int)model.CountryId) == false)
        {
            model.Countries = await countryService.GetAllCountriesAsync();
            return View(model);
        }

        if(await regionService.RegionWithThisNameAndCountryExistsAsync(model.Name, (int)model.CountryId))
        {
            ModelState.AddModelError("Name", "There is already a region with this name in this country.");
        }

        if(!ModelState.IsValid)
        {
            model.Countries = await countryService.GetAllCountriesAsync();
            return View(model);
        }

        await regionService.AddRegionAsync(model.Name,(int)model.CountryId);

        return RedirectToAction("Index");
    }

    [HttpGet]
    public async Task<IActionResult> Index(int currentPage = 1, int pageSize = 15)
    {
        var totalRegions = await regionService.GetTotalRegionsAsync();
        var regions = await regionService.GetAllRegionsAsync(currentPage, pageSize);

        var model = new RegionIndexViewModel
        {
            Regions = regions,
            CurrentPage = currentPage,
            TotalPages = (int)Math.Ceiling(totalRegions / (double)pageSize)
        };

        return View(model);
    }

    public async Task<IActionResult> Edit(int id)
    {
        var model = await regionService.GetRegionByIdAsync(id);

        if (model == null)
        {
            return NotFound();
        }
        model.Countries = await countryService.GetAllCountriesAsync();

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(EditRegionViewModel model)
    {

        if (model == null) //TODO Check for null on every post
        {
            return BadRequest("Invalid request");
        }

        var country = await regionService.GetRegionByIdAsync(model.Id);

        if (model == null)
        {
            return NotFound();
        }

        if (await countryService.CountryExistsAsync(model.CountryId) == false)
        {
            return BadRequest();
        }

        if (await regionService.RegionWithThisNameAndCountryExistsAsync(model.Name, model.CountryId))
        {
            ModelState.AddModelError("Name", "There is already a region with this name in this country.");
        }

        if (!ModelState.IsValid)
        {
            model.Countries = await countryService.GetAllCountriesAsync();
            return View(model);
        }

        await regionService.EditRegionAsync(model);

        return RedirectToAction(nameof(Index)); //TODO Redirect to Region/Info/Id
    }
}
