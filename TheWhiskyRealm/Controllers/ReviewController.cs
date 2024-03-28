using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TheWhiskyRealm.Core.Contracts;
using TheWhiskyRealm.Core.Models.Review;

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

        await reviewService.AddReviewAsync(model, userId);

        return RedirectToAction("Details", "Whisky", new { id = model.WhiskyId });
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var userId = User.Id();

        if (await reviewService.ReviewExistAsync(id) == false)
        {
            return BadRequest(); //TODO Change to My Reviews
        }

        var review = await reviewService.GetReviewAsync(id);

        if (userId != review.UserId)
        {
            return Unauthorized();
        }

        var model = new ReviewFormModel
        {
            Content = review.Content,
            Recommend = review.Recommend,
            Title = review.Title,
            WhiskyId = review.WhiskyId,
        };

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(int id, ReviewFormModel model)
    {
        var userId = User.Id();

        if (userId == null)
        {
            return RedirectToPage("/Account/Login");
        }

        if (await reviewService.ReviewExistAsync(id) == false)
        {
            return BadRequest(); //TODO Change to My Reviews
        }

        var review = await reviewService.GetReviewAsync(id);

        if (userId != review.UserId)
        {
            return Unauthorized();
        }

        if (!ModelState.IsValid)
        {
            return View(model);
        }

        await reviewService.EditReviewAsync(id, model);

        return RedirectToAction("Details", "Whisky", new { id = model.WhiskyId });
    }

    public async Task<IActionResult> Delete(int id)
    {
        var userId = User.Id();

        if (userId == null)
        {
            return RedirectToPage("/Account/Login");
        }

        if (await reviewService.ReviewExistAsync(id) == false)
        {
            return BadRequest(); //TODO Change to My Reviews
        }

        var review = await reviewService.GetReviewAsync(id);

        if (userId != review.UserId)
        {
            return Unauthorized();
        }

        await reviewService.DeleteReviewAsync(id);

        return RedirectToAction("Details", "Whisky", new { id = review.WhiskyId });
    }
}