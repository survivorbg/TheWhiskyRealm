using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TheWhiskyRealm.Core.Contracts;
using TheWhiskyRealm.Core.Models.Whisky.Add;

namespace TheWhiskyRealm.Controllers;

public class WhiskyController : BaseController
{
    private readonly IWhiskyService whiskyService;
    private readonly IWhiskyTypeService whiskyTypeService;
    private readonly IRegionService regionService;
    private readonly IDistilleryService distilleryService;
    private readonly IReviewService reviewService;

    public WhiskyController(IWhiskyService whiskyService,
        IWhiskyTypeService whiskyTypeService,
        IRegionService regionService,
        IDistilleryService distilleryService,
        IReviewService reviewService)
    {
        this.whiskyService = whiskyService;
        this.whiskyTypeService = whiskyTypeService;
        this.regionService = regionService;
        this.distilleryService = distilleryService;
        this.reviewService = reviewService;
    }

    [HttpGet]
    public async Task<IActionResult> All()
    {
        var model = await whiskyService.GetPagedWhiskiesAsync(0, 9);


        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        if (!await whiskyService.WhiskyExistAsync(id))
        {
            return BadRequest();
        }

        var model = await whiskyService.GetWhiskyByIdAsync(id);
        model.Reviews = await reviewService.AllReviewsForWhiskyAsync(id);

        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> LoadMoreWhiskies(int skip, int take)
    {

        var whiskies = await whiskyService.GetMoreWhiskiesAsync(skip, take);


        return PartialView("_WhiskiesPartial", whiskies);
    }

    [Authorize(Roles = "Administrator")]
    [HttpGet]
    public async Task<IActionResult> Add()
    {
        var model = new WhiskyFormModel();

        model.WhiskyTypes = await whiskyTypeService.GetAllWhiskyTypesAsync();
        model.Distilleries = await distilleryService.GetAllDistilleriesAsync();

        return View(model);
    }

    [Authorize(Roles = "Administrator")]
    [HttpPost]
    public async Task<IActionResult> Add(WhiskyFormModel model)
    {
        if (model == null)
        {
            return BadRequest();
        }

        if (await distilleryService.DistilleryExistsAsync(model.DistilleryId) == false)
        {
            ModelState.AddModelError(nameof(model.DistilleryId), "Distillery does not exist!");
        }

        if (await whiskyTypeService.WhiskyTypeExistsAsync(model.WhiskyTypeId) == false)
        {
            ModelState.AddModelError(nameof(model.DistilleryId), "Whisky type does not exist!");
        }

        if (await whiskyTypeService.GetWhiskyTypeNameAsync(model.WhiskyTypeId) == "Bourbon" && model.Age < 2)
        {
            ModelState.AddModelError("Age", "Bourbon whiskeys must be at least 2 years old!");
        }

        if (await whiskyTypeService.GetWhiskyTypeNameAsync(model.WhiskyTypeId) != "Bourbon" && model.Age < 3)
        {
            ModelState.AddModelError("Age", "All whiskies, except Bourbon, should be at least 3 years old!");
        }

        if (!ModelState.IsValid)
        {
            model.WhiskyTypes = await whiskyTypeService.GetAllWhiskyTypesAsync();
            model.Distilleries = await distilleryService.GetAllDistilleriesAsync();
            return View(model);
        }

        await whiskyService.AddWhiskyAsync(model);

        return RedirectToAction(nameof(All));
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        if(await whiskyService.WhiskyExistAsync(id) == false)
        {
            return BadRequest();
        }

        var model = await whiskyService.GetWhiskyByIdForEditAsync(id);

        model.WhiskyTypes = await whiskyTypeService.GetAllWhiskyTypesAsync();
        model.Distilleries = await distilleryService.GetAllDistilleriesAsync();

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(int id, WhiskyFormModel model)
    {
        if (model == null)
        {
            return BadRequest();
        }

        if (await whiskyService.WhiskyExistAsync(id) == false)
        {
            return BadRequest();
        }

        if (await distilleryService.DistilleryExistsAsync(model.DistilleryId) == false)
        {
            ModelState.AddModelError(nameof(model.DistilleryId), "Distillery does not exist!");
        }

        if (await whiskyTypeService.WhiskyTypeExistsAsync(model.WhiskyTypeId) == false)
        {
            ModelState.AddModelError(nameof(model.DistilleryId), "Whisky type does not exist!");
        }

        if (await whiskyTypeService.GetWhiskyTypeNameAsync(model.WhiskyTypeId) == "Bourbon" && model.Age < 2)
        {
            ModelState.AddModelError("Age", "Bourbon whiskeys must be at least 2 years old!");
        }

        if (await whiskyTypeService.GetWhiskyTypeNameAsync(model.WhiskyTypeId) != "Bourbon" && model.Age < 3)
        {
            ModelState.AddModelError("Age", "All whiskies, except Bourbon, should be at least 3 years old!");
        }

        if (!ModelState.IsValid)
        {
            model.WhiskyTypes = await whiskyTypeService.GetAllWhiskyTypesAsync();
            model.Distilleries = await distilleryService.GetAllDistilleriesAsync();
            return View(model);
        }

        await whiskyService.EditWhiskyAsync(id, model);

        return RedirectToAction("Details", new {id});
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        if(await whiskyService.WhiskyExistAsync(id) == false)
        {
            return BadRequest();
        }

        var model = await whiskyService.GetWhiskyByIdForEditAsync(id);

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int id, WhiskyFormModel model)
    {
        if (await whiskyService.WhiskyExistAsync(id) == false)
        {
            return BadRequest();
        }

        await whiskyService.DeleteAsync(id);


        return RedirectToAction(nameof(All));
    }
}
