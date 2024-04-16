using Microsoft.AspNetCore.Mvc;
using TheWhiskyRealm.Core.Contracts;
using TheWhiskyRealm.Core.Models.AdminArea.Country;
using TheWhiskyRealm.Core.Models.AdminArea.Region;
using static TheWhiskyRealm.Core.Constants.ControllerConstants;

namespace TheWhiskyRealm.Areas.Admin.Controllers;

public class RegionController : AdminBaseController
{
    private readonly IRegionService regionService;
    private readonly ICountryService countryService;
    private readonly IDistilleryService distilleryService;

    public RegionController(IRegionService regionService,
        ICountryService countryService,
        IDistilleryService distilleryService)
    {
        this.regionService = regionService;
        this.countryService = countryService;
        this.distilleryService = distilleryService;
    }

    public async Task<IActionResult> Add(int? countryId)
    {
        var model = new AddRegionViewModel();

        if (countryId != null && await countryService.CountryExistsAsync((int)countryId) == false)
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
        if (model.CountryId == null || await countryService.CountryExistsAsync((int)model.CountryId) == false)
        {
            model.Countries = await countryService.GetAllCountriesAsync();
            return View(model);
        }

        if (await regionService.RegionWithThisNameAndCountryExistsAsync(model.Name, (int)model.CountryId))
        {
            ModelState.AddModelError(nameof(model.Name), RegionWithThisNameInThisCountryMessage);
        }

        if (!ModelState.IsValid)
        {
            model.Countries = await countryService.GetAllCountriesAsync();
            return View(model);
        }

        var id = await regionService.AddRegionAsync(model.Name, (int)model.CountryId);

        return RedirectToAction(nameof(Info), "Region", new { id });
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

    [HttpGet]
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
            return BadRequest();
        }

        var region = await regionService.GetRegionByIdAsync(model.Id);

        if (region == null)
        {
            return NotFound();
        }

        if (await countryService.CountryExistsAsync(model.CountryId) == false)
        {
            return BadRequest();
        }

        if (await regionService.RegionWithThisNameAndCountryExistsAsync(model.Name, model.CountryId, model.Id))
        {
            ModelState.AddModelError(nameof(model.Name), RegionWithThisNameInThisCountryMessage);
        }

        if (!ModelState.IsValid)
        {
            model.Countries = await countryService.GetAllCountriesAsync();
            return View(model);
        }

        await regionService.EditRegionAsync(model);

        return RedirectToAction(nameof(Info), "Region", new { id = model.Id });
    }

    [HttpGet]
    public async Task<IActionResult> Info(int id)
    {
        var region = await regionService.GetRegionByIdAsync(id);

        if (region == null)
        {
            return NotFound();
        }

        var model = new RegionInfoViewModel()
        {
            Id = id,
            Name = region.Name,
            Distilleries = await distilleryService.GetAllDistilleriesAsync(id)
        };

        return View(model);
    }
}
