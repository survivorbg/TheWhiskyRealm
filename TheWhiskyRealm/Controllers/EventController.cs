using Microsoft.AspNetCore.Mvc;

namespace TheWhiskyRealm.Controllers
{
    public class EventController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
