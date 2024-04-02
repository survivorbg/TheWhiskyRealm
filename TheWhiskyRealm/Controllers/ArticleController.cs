using Microsoft.AspNetCore.Mvc;

namespace TheWhiskyRealm.Controllers;

public class ArticleController : BaseController
{
    public IActionResult Index()
    {
        return View();
    }
}
