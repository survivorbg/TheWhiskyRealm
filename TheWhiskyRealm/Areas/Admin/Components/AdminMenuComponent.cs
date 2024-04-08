using Microsoft.AspNetCore.Mvc;

namespace TheWhiskyRealm.Areas.Admin.Components;

public class AdminMenuComponent : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync()
    {
        return await Task.FromResult<IViewComponentResult>(View());
    }
}
