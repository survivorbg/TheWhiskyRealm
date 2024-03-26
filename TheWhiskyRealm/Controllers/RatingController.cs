using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TheWhiskyRealm.Core.Contracts;
using TheWhiskyRealm.Core.Models.Rating;

namespace TheWhiskyRealm.Controllers
{
    public class RatingController : BaseController
    {
        private readonly IRatingService ratingService;
        private readonly IWhiskyService whiskyService;

        public RatingController(IRatingService ratingService, IWhiskyService whiskyService)
        {
            this.ratingService = ratingService;
            this.whiskyService = whiskyService;
        }

        [HttpGet]
        public async Task<IActionResult> Rate(int id)
        {
            var userId = User.Id();
            if (userId == null)
            {
                return RedirectToPage("/Account/Login");
            }

            if (await whiskyService.WhiskyExistAsync(id) == false)
            {
                return BadRequest();
            }

            if (await ratingService.UserAlreadyGaveRatingAsync(userId, id))
            {
                RedirectToAction("Details", "Whisky", new { id });
            }

            var model = new RatingViewModel()
            {
                WhiskyId = id,
            };

            return View(model);
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
                RedirectToAction("Details", "Whisky", new { id = model.WhiskyId });
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await ratingService.RateAsync(userId, model);

            return RedirectToAction("Details", "Whisky", new { id = model.WhiskyId });
        }


    }
}
