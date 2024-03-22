using Microsoft.AspNetCore.Mvc;

namespace TheWhiskyRealm.Controllers;

public class WhiskyController : BaseController
{
    public async Task<IActionResult> All()
    {



        return View();
    }
}
