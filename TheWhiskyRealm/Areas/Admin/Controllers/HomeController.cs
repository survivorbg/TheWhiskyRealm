using Microsoft.AspNetCore.Mvc;

namespace TheWhiskyRealm.Areas.Admin.Controllers;

public class HomeController : AdminBaseController
{
    public IActionResult Index()
    {
        return View();
    }

}
