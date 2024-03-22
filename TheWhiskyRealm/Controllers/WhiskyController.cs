using Microsoft.AspNetCore.Mvc;
using TheWhiskyRealm.Core.Contracts;

namespace TheWhiskyRealm.Controllers;

public class WhiskyController : BaseController
{
    private readonly IWhiskyService whiskyService;

    public WhiskyController(IWhiskyService whiskyService)
    {
        this.whiskyService = whiskyService;
    }

    [HttpGet]
    public async Task<IActionResult> All()
    {
        var model = await whiskyService.AllWhiskiesAsync();


        return View(model);
    }
}
