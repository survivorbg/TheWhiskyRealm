using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TheWhiskyRealm.Core.Contracts;
using TheWhiskyRealm.Core.Models.Award;
using TheWhiskyRealm.Infrastructure.Data.Enums;
using static TheWhiskyRealm.Core.Constants.ControllerConstants;
using static TheWhiskyRealm.Core.Constants.RoleConstants;

namespace TheWhiskyRealm.Controllers;

public class AwardController : BaseController
{
    private readonly IAwardService awardService;
    private readonly IWhiskyService whiskyService;

    public AwardController(IAwardService awardService, IWhiskyService whiskyService)
    {
        this.awardService = awardService;
        this.whiskyService = whiskyService;
    }

    [Authorize(Roles = $"{Administrator},{WhiskyExpert}")]
    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var userId = User.Id(); //TODO Remove all unnecessary validations like this

        if (await awardService.AwardExistAsync(id) == false)
        {
            return NotFound();
        }

        var model = await awardService.GetAwardByIdAsync(id);

        var publisherId = await whiskyService.GetWhiskyPublisherAsync(model.WhiskyId);

        if (User.IsInRole(WhiskyExpert) && publisherId != User.Id())
        {
            return Unauthorized();
        }

        model.MedalTypeOptions = Enum.GetNames(typeof(MedalType));

        return View(model);
    }

    [Authorize(Roles = $"{Administrator},{WhiskyExpert}")]
    [HttpPost]
    public async Task<IActionResult> Edit(AwardViewModel model)
    {
        var userId = User.Id();
        var publisherId = await whiskyService.GetWhiskyPublisherAsync(model.WhiskyId);

        if (User.IsInRole(WhiskyExpert) && publisherId != User.Id())
        {
            return Unauthorized();
        }

        if (await awardService.AwardExistAsync(model.Id) == false)
        {
            return NotFound();
        }

        model.MedalTypeOptions = Enum.GetNames(typeof(MedalType));

        if (!model.MedalTypeOptions.Contains(model.MedalType))
        {
            ModelState.AddModelError(nameof(model.MedalType), InvalidMedalType);
            return View(model);
        }

        if (!ModelState.IsValid)
        {
            return View(model);
        }
        await awardService.EditAwardAsync(model);

        return RedirectToAction(nameof(WhiskyController.Details), "Whisky", new { id = model.WhiskyId });
    }

    [Authorize(Roles = $"{Administrator},{WhiskyExpert}")]
    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        var userId = User.Id();

        var award = await awardService.GetAwardByIdAsync(id);
        if (award == null)
        {
            return NotFound();
        }

        var publisherId = await whiskyService.GetWhiskyPublisherAsync(award.WhiskyId);

        if (User.IsInRole(WhiskyExpert) && publisherId != userId)
        {
            return Unauthorized();
        }

        return View(award);
    }

    [Authorize(Roles = $"{Administrator},{WhiskyExpert}")]
    [HttpPost]
    public async Task<IActionResult> Delete(AwardViewModel model)
    {
        var userId = User.Id();

        var award = await awardService.GetAwardByIdAsync(model.Id);
        if (award == null)
        {
            return NotFound();
        }

        var publisherId = await whiskyService.GetWhiskyPublisherAsync(award.WhiskyId);

        if (User.IsInRole(WhiskyExpert) && publisherId != userId)
        {
            return Unauthorized();
        }

        await awardService.DeleteAwardAsync(model.Id);

        return RedirectToAction(nameof(WhiskyController.Details), "Whisky", new { id = award.WhiskyId });
    }

    [Authorize(Roles = $"{Administrator},{WhiskyExpert}")]
    [HttpGet]
    public async Task<IActionResult> Add(int id)
    {
        var userId = User.Id();

        if (await whiskyService.WhiskyExistAsync(id) == false)
        {
            return NotFound();
        }

        var publisherId = await whiskyService.GetWhiskyPublisherAsync(id);

        if (User.IsInRole(WhiskyExpert) && publisherId != userId)
        {
            return Unauthorized();
        }

        var model = new AwardAddModel()
        {
            MedalTypeOptions = Enum.GetNames(typeof(MedalType)),
            WhiskyId = id
        };


        return View(model);
    }

    [Authorize(Roles = $"{Administrator},{WhiskyExpert}")]
    [HttpPost]
    public async Task<IActionResult> Add(AwardAddModel model)
    {
        var userId = User.Id();

        if (await whiskyService.WhiskyExistAsync(model.WhiskyId) == false)
        {
            return NotFound();
        }

        var publisherId = await whiskyService.GetWhiskyPublisherAsync(model.WhiskyId);

        if (User.IsInRole(WhiskyExpert) && publisherId != userId)
        {
            return Unauthorized();
        }

        model.MedalTypeOptions = Enum.GetNames(typeof(MedalType));

        if (!model.MedalTypeOptions.Contains(model.MedalType))
        {
            ModelState.AddModelError(nameof(model.MedalType), InvalidMedalType);
            return View(model);
        }

        if (!ModelState.IsValid)
        {
            return View(model);
        }

        await awardService.AddAwardAsync(model);

        return RedirectToAction(nameof(WhiskyController.Details), "Whisky", new { id = model.WhiskyId });
    }
}
