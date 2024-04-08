﻿using Microsoft.AspNetCore.Mvc;

namespace TheWhiskyRealm.Components;

public class MainMenuComponent : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync()
    {
        return await Task.FromResult<IViewComponentResult>(View());
    }
}
