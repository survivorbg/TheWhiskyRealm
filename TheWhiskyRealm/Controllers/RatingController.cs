using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TheWhiskyRealm.Core.Contracts;
using TheWhiskyRealm.Core.Models.Rating;

namespace TheWhiskyRealm.Controllers;

public class RatingController : BaseController
{
    private readonly IRatingService ratingService;
    private readonly IWhiskyService whiskyService;

    public RatingController(IRatingService ratingService, IWhiskyService whiskyService)
    {
        this.ratingService = ratingService;
        this.whiskyService = whiskyService;
    }

    [HttpPost]
    public async Task<IActionResult> Rate(RatingViewModel model)
    {
        var userId = User.Id();
        if (userId == null)
        {
            return RedirectToPage("/Account/Login");
        }

        if (await whiskyService.WhiskyExistAsync(model.WhiskyId) == false)
        {
            return RedirectToAction("All", "Whisky");
        }

        if (!ModelState.IsValid)
        {
            return View(model);
        }

        await ratingService.RateAsync(userId, model);

        return RedirectToAction("Details", "Whisky", new { id = model.WhiskyId });
    }
}
