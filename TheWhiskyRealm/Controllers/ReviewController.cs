﻿using Microsoft.AspNetCore.Mvc;
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
        if (await whiskyService.WhiskyExistAsync(model.WhiskyId) == false ||
            await whiskyService.WhiskyIsApprovedAsync(model.WhiskyId) == false)
        {
            return NotFound();
        }

        var userId = User.Id();

        if (await reviewService.UserAlreadyReviewedWhiskyAsync(userId, model.WhiskyId))
        {
            var reviewId = await reviewService.GetReviewIdAsync(userId, model.WhiskyId);
            return RedirectToAction("Edit", new { id = reviewId });
        }

        if (!ModelState.IsValid)
        {
            return View(model);
        }

        await reviewService.AddReviewAsync(model, userId);

        return RedirectToAction(nameof(WhiskyController.Details), "Whisky", new { id = model.WhiskyId });
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var userId = User.Id();
        var review = await reviewService.GetReviewAsync(id);

        if (review == null)
        {
            return NotFound();
        }

        if (await whiskyService.WhiskyExistAsync(review.WhiskyId) == false ||
            await whiskyService.WhiskyIsApprovedAsync(review.WhiskyId) == false)
        {
            return NotFound();
        }

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
        var review = await reviewService.GetReviewAsync(id);

        if (review == null)
        {
            return NotFound();
        }

        if (await whiskyService.WhiskyExistAsync(review.WhiskyId) == false ||
            await whiskyService.WhiskyIsApprovedAsync(review.WhiskyId) == false)
        {
            return NotFound();
        }
        var userId = User.Id();
        if (userId != review.UserId)
        {
            return Unauthorized();
        }

        if (!ModelState.IsValid)
        {
            return View(model);
        }

        await reviewService.EditReviewAsync(id, model);

        return RedirectToAction(nameof(WhiskyController.Details), "Whisky", new { id = model.WhiskyId });
    }

    public async Task<IActionResult> Delete(int id)
    {
        var userId = User.Id();

        var review = await reviewService.GetReviewAsync(id);

        if (review == null)
        {
            return NotFound();
        }

        if (await whiskyService.WhiskyExistAsync(review.WhiskyId) == false ||
            await whiskyService.WhiskyIsApprovedAsync(review.WhiskyId) == false)
        {
            return NotFound();
        }

        if (userId != review.UserId && !User.IsAdmin())
        {
            return Unauthorized();
        }

        await reviewService.DeleteReviewAsync(id);

        return RedirectToAction(nameof(WhiskyController.Details), "Whisky", new { id = review.WhiskyId });
    }
    [HttpGet]
    public async Task<IActionResult> MyReviews()
    {
        var userId = User.Id();

        var model = await reviewService.AllUserReviewsAsync(userId);

        return View(model);
    }

}