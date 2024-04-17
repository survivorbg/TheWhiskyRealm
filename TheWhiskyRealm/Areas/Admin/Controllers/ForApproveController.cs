using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TheWhiskyRealm.Controllers;
using TheWhiskyRealm.Core.Contracts;


namespace TheWhiskyRealm.Areas.Admin.Controllers
{
    public class ForApprove : AdminBaseController
    {
        private readonly IWhiskyService whiskyService;

        public ForApprove(IWhiskyService whiskyService)
        {
            this.whiskyService = whiskyService;
        }
        
        [HttpPost]
        public async Task<IActionResult> Approve(int id)
        {
            var whisky = await whiskyService.GetWhiskyByIdForEditAsync(id);

            if (whisky == null)
            {
                return NotFound();
            }

            await whiskyService.ApproveWhiskyAsync(id);

            return RedirectToAction("Details", "Whisky", new { area = "", id });
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var model = await whiskyService.GetAllWhiskiesForApproveAsync();

            return View(model);
        }
    }
}
