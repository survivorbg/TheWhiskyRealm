﻿using Microsoft.AspNetCore.Mvc;
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

        if (await whiskyService.WhiskyExistAsync(model.WhiskyId) == false ||
            await whiskyService.WhiskyIsApprovedAsync(model.WhiskyId) == false)
        {
            return NotFound();
        }

        if (!ModelState.IsValid)
        {
            return View(model);
        }

        await ratingService.RateAsync(userId, model);

        return RedirectToAction(nameof(WhiskyController.Details), "Whisky", new { id = model.WhiskyId });
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var userId = User.Id();

        var model = await ratingService.GetRatingAsync(userId, id);

        if (model == null)
        {
            return NotFound();
        }

        if (await whiskyService.WhiskyExistAsync(model.WhiskyId) == false ||
            await whiskyService.WhiskyIsApprovedAsync(model.WhiskyId) == false)
        {
            return NotFound();
        }

        var viewModel = new RatingViewModel
        {
            WhiskyId = id,
            Nose = model.Nose,
            Taste = model.Taste,
            Finish = model.Finish
        };

        return PartialView("_RatingEditFormPartial", viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> Update(RatingViewModel model)
    {
        var userId = User.Id();

        if (await whiskyService.WhiskyExistAsync(model.WhiskyId) == false ||
            await whiskyService.WhiskyIsApprovedAsync(model.WhiskyId) == false)
        {
            return NotFound();
        }

        var rating = await ratingService.GetRatingAsync(userId, model.WhiskyId);

        if (rating == null)
        {
            return NotFound();
        }

        if (!ModelState.IsValid) 
        {
            return RedirectToAction(nameof(WhiskyController.Details), "Whisky", new { id = model.WhiskyId });
        }

        await ratingService.EditRatingAsync(model, rating.Id);

        return RedirectToAction(nameof(WhiskyController.Details), "Whisky", new { id = model.WhiskyId });
    }

    [HttpGet]
    public async Task<IActionResult> MyRatings()
    {
        var userId = User.Id();

        var model = await ratingService.GetRatingsByUserAsync(userId);

        return View(model);
    }

}
