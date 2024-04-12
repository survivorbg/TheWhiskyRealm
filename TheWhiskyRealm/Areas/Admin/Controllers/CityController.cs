using Microsoft.AspNetCore.Mvc;
using TheWhiskyRealm.Core.Contracts;

namespace TheWhiskyRealm.Areas.Admin.Controllers
{
    public class CityController : AdminBaseController
    {
        private readonly ICountryService countryService;

        public IActionResult Index()
        {
            return View();
        }
    }
}
