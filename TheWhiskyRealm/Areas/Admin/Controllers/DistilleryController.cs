using Microsoft.AspNetCore.Mvc;
using TheWhiskyRealm.Core.Contracts;
using TheWhiskyRealm.Core.Models.AdminArea.Country;
using TheWhiskyRealm.Core.Models.AdminArea.Distillery;
using TheWhiskyRealm.Core.Models.AdminArea.Region;
using TheWhiskyRealm.Core.Services;

namespace TheWhiskyRealm.Areas.Admin.Controllers;

public class DistilleryController : AdminBaseController
{
    private readonly IDistilleryService distilleryService;

    public DistilleryController(IDistilleryService distilleryService)
    {
        this.distilleryService = distilleryService;
    }

    [HttpGet]
    public async Task<IActionResult> Index(int currentPage = 1, int pageSize = 20, string sortOrder = "")
    {
        ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
        ViewData["RegionSortParm"] = sortOrder == "Region" ? "region_desc" : "Region";
        ViewData["CountrySortParm"] = sortOrder == "Country" ? "country_desc" : "Country";
        ViewData["YearSortParm"] = sortOrder == "Year" ? "year_desc" : "Year";
        ViewData["CurrentSort"] = sortOrder;

        var totalDistilleries = await distilleryService.GetTotalDistilleriesAsync();
        var distilleries = await distilleryService.GetAllDistilleriesAsync(currentPage, pageSize, sortOrder);

        var model = new DistilleryIndexViewModel
        {
            Distilleries = distilleries,
            CurrentPage = currentPage,
            TotalPages = (int)Math.Ceiling(totalDistilleries / (double)pageSize)
        };

        return View(model);
    }
}
