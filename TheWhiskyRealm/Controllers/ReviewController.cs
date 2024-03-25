using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TheWhiskyRealm.Core.Contracts;
using TheWhiskyRealm.Core.Models.Review;
using TheWhiskyRealm.Infrastructure.Data.Models;

namespace TheWhiskyRealm.Controllers;

public class ReviewController : BaseController
{
    private readonly IWhiskyService whiskyService;
    private readonly IReviewService reviewService;

    public ReviewController(IWhiskyService whiskyService, IReviewService reviewService)
    {
        this.whiskyService = whiskyService;
        this.reviewService = reviewService;
    }

    //SeeAllReviews
    [HttpGet]
    public async Task<IActionResult> Add(int id)
    {
        var userId = User.Id();
        if (userId == null)
        {
            return RedirectToPage("/Account/Login");
        }

        if(await reviewService.UserAlreadyReviewedWhiskyAsync(userId,id))
        {
            var reviewId = reviewService.GetReviewIdAsync(userId, id);
            return RedirectToPage("Edit", new {id=reviewId});
        }

        if (await whiskyService.WhiskyExistAsync(id) == false)
        {
            return BadRequest();
        }

        var whisky = await whiskyService.GetWhiskyByIdAsync(id);

        var model = new ReviewFormModel()
        {
            WhiskyId = id
        };

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Add(ReviewFormModel model)
    {
        var userId = User.Id();
        if (userId == null)
        {
            return RedirectToPage("/Account/Login");
        }

        if (await reviewService.UserAlreadyReviewedWhiskyAsync(userId, model.WhiskyId))
        {
            var reviewId = reviewService.GetReviewIdAsync(userId, model.WhiskyId);
            return RedirectToPage("Edit", new { id = reviewId });
        }

        if (await whiskyService.WhiskyExistAsync(model.WhiskyId) == false)
        {
            return BadRequest();
        }
        
        if (!ModelState.IsValid)
        {
            return View(model);
        }
        
        await reviewService.AddReviewAsync(model,userId);

        return RedirectToAction("Details","Whisky",new {id=model.WhiskyId});
    }
}