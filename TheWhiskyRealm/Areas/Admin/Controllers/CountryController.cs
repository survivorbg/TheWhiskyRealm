using Microsoft.AspNetCore.Mvc;
using TheWhiskyRealm.Core.Contracts;

namespace TheWhiskyRealm.Areas.Admin.Controllers;

public class CountryController : AdminBaseController
{
    private readonly ICountryService countryService;

    public CountryController(ICountryService countryService)
    {
        this.countryService = countryService;
    }

    public async Task<IActionResult> Index()
    {
        var model = await countryService.GetAllCountriesAsync();

        return View(model);
    }
}
