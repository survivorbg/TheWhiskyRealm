using Microsoft.AspNetCore.Mvc;
using TheWhiskyRealm.Core.Contracts;

namespace TheWhiskyRealm.Controllers
{
    public class RatingController : BaseController
    {
        private readonly IRatingService ratingService;

        public RatingController(IRatingService ratingService)
        {
            this.ratingService = ratingService;
        }

    }
}
