using Microsoft.AspNetCore.Mvc;
using TheWhiskyRealm.Core.Contracts;
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
}
