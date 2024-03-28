using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TheWhiskyRealm.Core.Contracts;
using TheWhiskyRealm.Core.Models.Award;
using TheWhiskyRealm.Core.Models.Whisky;
using TheWhiskyRealm.Infrastructure.Data.Enums;

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

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var userId = User.Id();
        if (userId == null)
        {
            return RedirectToPage("/Account/Login");
        }

        if (await awardService.AwardExistAsync(id) == false)
        {
            return BadRequest();
        }

        var model = await awardService.GetAwardByIdAsync(id);

        model.MedalTypeOptions = Enum.GetNames(typeof(MedalType));

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(AwardViewModel model)
    {
        var userId = User.Id();
        if (userId == null)
        {
            return RedirectToPage("/Account/Login");
        }

        if (await awardService.AwardExistAsync(model.Id) == false)
        {
            return BadRequest();
        }

        if (model.MedalType != "Gold" && model.MedalType != "Silver" && model.MedalType != "Bronze")
        {
            ModelState.AddModelError(nameof(AwardViewModel.MedalType), "Invalid medal type.");
            model.MedalTypeOptions = Enum.GetNames(typeof(MedalType));
            return View(model);
        }

        if (!ModelState.IsValid)
        {
            model.MedalTypeOptions = Enum.GetNames(typeof(MedalType));
            return View(model);
        }

        await awardService.EditAwardAsync(model);

        return RedirectToAction("Details", "Whisky", new { id = model.WhiskyId });
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        var userId = User.Id();
        if (userId == null)
        {
            return RedirectToPage("/Account/Login");
        }

        var award = await awardService.GetAwardByIdAsync(id);
        if (award == null)
        {
            return NotFound();
        }

        return View(award);
    }

    [HttpPost]
    public async Task<IActionResult> Delete(AwardViewModel model)
    {
        var userId = User.Id();
        if (userId == null)
        {
            return RedirectToPage("/Account/Login");
        }

        var award = await awardService.GetAwardByIdAsync(model.Id);
        if (award == null)
        {
            return NotFound();
        }

        await awardService.DeleteAwardAsync(model.Id);

        return RedirectToAction("Details", "Whisky", new { id = award.WhiskyId });
    }

    [HttpGet]
    public async Task<IActionResult> Add(int id)
    {
        var userId = User.Id();
        if (userId == null)
        {
            return RedirectToPage("/Account/Login");
        }

        if (await whiskyService.WhiskyExistAsync(id) == false)
        {
            return NotFound();
        }
        var model = new AwardAddModel()
        {
            MedalTypeOptions = Enum.GetNames(typeof(MedalType)),
            WhiskyId = id
        };


        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Add(AwardAddModel model)
    {
        var userId = User.Id();
        if (userId == null)
        {
            return RedirectToPage("/Account/Login");
        }

        if (await whiskyService.WhiskyExistAsync(model.WhiskyId) == false)
        {
            return NotFound();
        }

        if (model.MedalType != "Gold" && model.MedalType != "Silver" && model.MedalType != "Bronze")
        {
            ModelState.AddModelError(nameof(AwardViewModel.MedalType), "Invalid medal type.");
            model.MedalTypeOptions = Enum.GetNames(typeof(MedalType));
            return View(model);
        }

        if (!ModelState.IsValid)
        {
            model.MedalTypeOptions = Enum.GetNames(typeof(MedalType));
            return View(model);
        }

        await awardService.AddAwardAsync(model);

        return RedirectToAction("Details", "Whisky", new {id=model.WhiskyId});
    }
}
