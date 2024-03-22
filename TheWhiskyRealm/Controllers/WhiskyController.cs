using Microsoft.AspNetCore.Mvc;
using TheWhiskyRealm.Core.Contracts;
using TheWhiskyRealm.Core.Services;

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
        var model = await whiskyService.GetPagedWhiskiesAsync(0,9);


        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        if(!await whiskyService.WhiskyExistAsync(id))
        {
            return BadRequest();
        }

        var model = await whiskyService.GetWhiskyByIdAsync(id);

        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> LoadMoreWhiskies(int skip, int take)
    {
        
        var whiskies = await whiskyService.GetMoreWhiskiesAsync(skip, take);

        
        return PartialView("_WhiskiesPartial", whiskies);
    }
}
