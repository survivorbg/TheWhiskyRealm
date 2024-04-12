using Microsoft.AspNetCore.Mvc;
using TheWhiskyRealm.Core.Contracts;
using TheWhiskyRealm.Core.Models.AdminArea.Distillery;

namespace TheWhiskyRealm.Areas.Admin.Controllers;

public class DistilleryController : AdminBaseController
{
    private readonly IDistilleryService distilleryService;
    private readonly IWhiskyService whiskyService;
    private readonly IRegionService regionService;

    public DistilleryController(IDistilleryService distilleryService,
        IWhiskyService whiskyService,
        IRegionService regionService)
    {
        this.distilleryService = distilleryService;
        this.whiskyService = whiskyService;
        this.regionService = regionService;
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

    [HttpGet]
    public async Task<IActionResult> Info(int id)
    {
        var model = await distilleryService.GetDistilleryInfoAsync(id);

        if (model == null)
        {
            return NotFound();
        }

        model.Whiskies = await whiskyService.GetWhiskiesByDistilleryIdAsync(id);

        return View(model);
    }

    public async Task<IActionResult> Add()
    {
        var model = new DistilleryAddViewModel();
        model.Regions = await regionService.GetAllRegionsAsync();
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Add(DistilleryAddViewModel model)
    {
        if (await distilleryService.DistilleryExistByName(model.Name))
        {
            ModelState.AddModelError("Name", "There is already a distillery with that name.");
        }

        if (!ModelState.IsValid)
        {
            model.Regions = await regionService.GetAllRegionsAsync();
            return View(model);
        }

        await distilleryService.AddDistilleryAsync(model);

        return RedirectToAction("Index");
    }
}
