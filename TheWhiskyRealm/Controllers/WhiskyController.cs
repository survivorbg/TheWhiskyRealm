using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TheWhiskyRealm.Core.Contracts;
using TheWhiskyRealm.Core.Models.Review;
using TheWhiskyRealm.Core.Models.Whisky.Add;

namespace TheWhiskyRealm.Controllers;

public class WhiskyController : BaseController
{
    private readonly IWhiskyService whiskyService;
    private readonly IWhiskyTypeService whiskyTypeService;
    private readonly IRegionService regionService;
    private readonly IDistilleryService distilleryService;
    private readonly IReviewService reviewService;
    private readonly IAwardService awardService;

    public WhiskyController(IWhiskyService whiskyService,
        IWhiskyTypeService whiskyTypeService,
        IRegionService regionService,
        IDistilleryService distilleryService,
        IReviewService reviewService,
        IAwardService awardService)
    {
        this.whiskyService = whiskyService;
        this.whiskyTypeService = whiskyTypeService;
        this.regionService = regionService;
        this.distilleryService = distilleryService;
        this.reviewService = reviewService;
        this.awardService = awardService;
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
        model.Review = new ReviewFormModel()
        {
            WhiskyId = id
        };
        model.Awards = await awardService.GetAllWhiskyAwards(id);
        model.IsFavourite = await whiskyService.WhiskyInFavouritesAsync(User.Id(), id);

        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> LoadMoreWhiskies(int skip, int take, string sortOrder)
    {
        var whiskies = await whiskyService.GetPagedWhiskiesAsync(skip, take, sortOrder);

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
        if (await whiskyService.WhiskyExistAsync(id) == false)
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

        return RedirectToAction("Details", new { id });
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        if (await whiskyService.WhiskyExistAsync(id) == false)
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

    [HttpPost]
    public async Task<IActionResult> AddToFavourites(int id)
    {
        var userId = User.Id();


        if (await whiskyService.WhiskyInFavouritesAsync(userId, id))
        {
            return BadRequest("Whisky is already in favourites.");
        }


        await whiskyService.AddToFavouritesAsync(userId, id);

        return Ok();
    }
    [HttpPost]
    public async Task<IActionResult> RemoveFromFavourites(int id)
    {
        var userId = User.Id();


        if (await whiskyService.WhiskyInFavouritesAsync(userId, id) == false)
        {
            return BadRequest("Whisky is already in favourites.");
        }


        await whiskyService.RemoveFromFavouritesAsync(userId, id);
        
        return Ok();
    }

    [HttpGet]
    public async Task<IActionResult> MyCollection()
    {
        var userId = User.Id();
        if (userId == null)
        {
            return BadRequest();
        }

        var model = await whiskyService.MyFavouriteWhiskiesAsync(userId);

        return View(model);
    }

}
