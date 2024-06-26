﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TheWhiskyRealm.Core.Contracts;
using TheWhiskyRealm.Core.Models.AdminArea.Distillery;
using static TheWhiskyRealm.Core.Constants.ControllerConstants;

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

    [HttpGet]
    public async Task<IActionResult> Add()
    {
        var model = new DistilleryFormViewModel();
        model.Regions = await regionService.GetAllRegionsAsync();
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Add(DistilleryFormViewModel model)
    {
        if (await distilleryService.DistilleryExistByName(model.Name))
        {
            ModelState.AddModelError(nameof(model.Name), DistilleryWithThatNameMessage);
        }

        if (!ModelState.IsValid)
        {
            model.Regions = await regionService.GetAllRegionsAsync();
            return View(model);
        }

        var id = await distilleryService.AddDistilleryAsync(model);

        return RedirectToAction(nameof(Info), "Distillery", new {id});
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var model = await distilleryService.GetDistilleryByIdAsync(id);

        if (model == null)
        {
            return NotFound();
        }

        model.Regions = await regionService.GetAllRegionsAsync();

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(DistilleryFormViewModel model)
    {

        if (model == null) 
        {
            return BadRequest();
        }

        var distillery = await distilleryService.GetDistilleryByIdAsync(model.Id);

        if (distillery == null)
        {
            return NotFound();
        }

        if (await regionService.RegionExistsAsync(model.RegionId) == false)
        {
            return BadRequest();
        }

        if (await distilleryService.DistilleryExistByName(model.Name,model.Id))
        {
            ModelState.AddModelError(nameof(model.Name), DistilleryWithThatNameMessage);
        }

        if (!ModelState.IsValid)
        {
            model.Regions = await regionService.GetAllRegionsAsync();
            return View(model);
        }

        await distilleryService.EditDistilleryAsync(model);

        return RedirectToAction(nameof(Info), "Distillery", new { model.Id });
    }

    [Route("Distillery/Details/{id}")]
    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> Details(int id)
    {
        var model = await distilleryService.GetDistilleryInfoAsync(id);

        if (model == null)
        {
            return NotFound();
        }

        return View(model);
    }
}
