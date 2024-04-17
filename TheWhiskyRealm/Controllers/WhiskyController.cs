using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TheWhiskyRealm.Core.Contracts;
using TheWhiskyRealm.Core.Models.Review;
using TheWhiskyRealm.Core.Models.Whisky;
using TheWhiskyRealm.Core.Models.Whisky.Add;
using static TheWhiskyRealm.Core.Constants.RoleConstants;
using static TheWhiskyRealm.Core.Constants.ControllerConstants;

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
        if (await whiskyService.WhiskyExistAsync(id) == false)
        {
            return NotFound();
        }

        if (await whiskyService.WhiskyIsApprovedAsync(id) == false && !User.IsAdmin())
        {
            return Unauthorized();
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


    [Authorize(Roles = $"{Administrator},{WhiskyExpert}")]
    [HttpGet]
    public async Task<IActionResult> Add()
    {
        var model = new WhiskyFormModel();

        model.WhiskyTypes = await whiskyTypeService.GetAllWhiskyTypesAsync();
        model.Distilleries = await distilleryService.GetAllDistilleriesAsync();

        return View(model);
    }

    [Authorize(Roles = $"{Administrator},{WhiskyExpert}")]
    [HttpPost]
    public async Task<IActionResult> Add(WhiskyFormModel model)
    {
        if (model == null)
        {
            return BadRequest();
        }

        if (await distilleryService.DistilleryExistsAsync(model.DistilleryId) == false)
        {
            ModelState.AddModelError(nameof(model.DistilleryId), DistilleryDoesNotExist);
        }

        if (await whiskyTypeService.WhiskyTypeExistsAsync(model.WhiskyTypeId) == false)
        {
            ModelState.AddModelError(nameof(model.DistilleryId), WhiskyTypeDoesNotExist);
        }

        if (await whiskyTypeService.GetWhiskyTypeNameAsync(model.WhiskyTypeId) == Bourbon && model.Age < minAgeForBourbon)
        {
            ModelState.AddModelError(nameof(model.Age), BourbonAgeRequirement);
        }

        if (await whiskyTypeService.GetWhiskyTypeNameAsync(model.WhiskyTypeId) != Bourbon && model.Age < minAgeForWhisky)
        {
            ModelState.AddModelError(nameof(model.Age), WhiskyAgeRequirement);
        }

        if (!ModelState.IsValid)
        {
            model.WhiskyTypes = await whiskyTypeService.GetAllWhiskyTypesAsync();
            model.Distilleries = await distilleryService.GetAllDistilleriesAsync();
            return View(model);
        }

        if (User.IsInRole(Administrator))
        {
            model.IsApproved = true;
        }
        await whiskyService.AddWhiskyAsync(model);

        return RedirectToAction(nameof(All));
    }

    [Authorize(Roles = $"{Administrator},{WhiskyExpert}")]
    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        if (await whiskyService.WhiskyExistAsync(id) == false)
        {
            return NotFound();
        }

        var model = await whiskyService.GetWhiskyByIdForEditAsync(id);

        if (User.IsInRole(WhiskyExpert) && model.PublishedBy != User.Id())
        {
            return Unauthorized();
        }

        model.WhiskyTypes = await whiskyTypeService.GetAllWhiskyTypesAsync();
        model.Distilleries = await distilleryService.GetAllDistilleriesAsync();

        return View(model);
    }

    [Authorize(Roles = $"{Administrator},{WhiskyExpert}")]
    [HttpPost]
    public async Task<IActionResult> Edit(int id, WhiskyFormModel model)
    {
        if (model == null)
        {
            return BadRequest();
        }

        if (await whiskyService.WhiskyExistAsync(id) == false)
        {
            return NotFound();
        }

        var publisherId = await whiskyService.GetWhiskyPublisherAsync(id);

        if (User.IsInRole(WhiskyExpert) && publisherId != User.Id())
        {
            return Unauthorized();
        }

        if (await distilleryService.DistilleryExistsAsync(model.DistilleryId) == false)
        {
            ModelState.AddModelError(nameof(model.DistilleryId), DistilleryDoesNotExist);
        }

        if (await whiskyTypeService.WhiskyTypeExistsAsync(model.WhiskyTypeId) == false)
        {
            ModelState.AddModelError(nameof(model.DistilleryId), WhiskyTypeDoesNotExist);
        }

        if (await whiskyTypeService.GetWhiskyTypeNameAsync(model.WhiskyTypeId) == Bourbon && model.Age < minAgeForBourbon)
        {
            ModelState.AddModelError(nameof(model.Age), BourbonAgeRequirement);
        }

        if (await whiskyTypeService.GetWhiskyTypeNameAsync(model.WhiskyTypeId) != Bourbon && model.Age < minAgeForWhisky)
        {
            ModelState.AddModelError(nameof(model.Age), WhiskyAgeRequirement);
        }

        if (!ModelState.IsValid)
        {
            model.WhiskyTypes = await whiskyTypeService.GetAllWhiskyTypesAsync();
            model.Distilleries = await distilleryService.GetAllDistilleriesAsync();
            return View(model);
        }

        if (User.IsInRole(WhiskyExpert))
        {
            model.IsApproved = false;
            await whiskyService.EditWhiskyAsync(id, model);
            return RedirectToAction(nameof(All));
        }

        await whiskyService.EditWhiskyAsync(id, model);

        return RedirectToAction(nameof(Details), new { id });
    }

    [Authorize(Roles = Administrator)]
    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        if (await whiskyService.WhiskyExistAsync(id) == false)
        {
            return NotFound();
        }

        var model = await whiskyService.GetWhiskyByIdForEditAsync(id);

        return View(model);
    }

    [Authorize(Roles = Administrator)]
    [HttpPost]
    public async Task<IActionResult> Delete(int id, WhiskyFormModel model)
    {
        if (await whiskyService.WhiskyExistAsync(id) == false)
        {
            return NotFound();
        }

        await whiskyService.DeleteAsync(id);


        return RedirectToAction(nameof(All));
    }

    [HttpPost]
    public async Task<IActionResult> AddToFavourites(int id)
    {
        var userId = User.Id();
        if (await whiskyService.WhiskyExistAsync(id) == false ||
            await whiskyService.WhiskyIsApprovedAsync(id) == false)
        {
            return NotFound();
        }

        if (await whiskyService.WhiskyInFavouritesAsync(userId, id))
        {
            return BadRequest();
        }


        await whiskyService.AddToFavouritesAsync(userId, id);

        return Ok();
    }
    [HttpPost]
    public async Task<IActionResult> RemoveFromFavourites(int id)
    {
        var userId = User.Id();

        if (await whiskyService.WhiskyExistAsync(id) == false ||
           await whiskyService.WhiskyIsApprovedAsync(id) == false)
        {
            return NotFound();
        }

        if (await whiskyService.WhiskyInFavouritesAsync(userId, id) == false)
        {
            return BadRequest();
        }


        await whiskyService.RemoveFromFavouritesAsync(userId, id);

        return Ok();
    }

    public async Task<IActionResult> MyCollection(int page = 1, int pageSize = 4)
    {
        var userId = User.Id();
        if (userId == null)
        {
            return BadRequest();
        }
        MyCollectionPagination model = new MyCollectionPagination();
        var allWhiskies = await whiskyService.MyFavouriteWhiskiesAsync(userId);
        model.page = page;
        model.pageSize = pageSize;
        model.allWhiskies = allWhiskies.Count();
        var paginatedWhiskies = allWhiskies.Skip((page - 1) * pageSize).Take(pageSize).ToList();
        model.Whiskies = paginatedWhiskies;
        return View(model);
    }
}
