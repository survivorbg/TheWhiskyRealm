using Microsoft.AspNetCore.Mvc;
using TheWhiskyRealm.Core.Contracts;

namespace TheWhiskyRealm.Controllers;

public class ArticleController : BaseController
{
    private readonly IArticleService articleService;

    public ArticleController(IArticleService articleService)
    {
        this.articleService = articleService;
    }

    public async Task<IActionResult> Index()
    {
        var model = await  articleService.GetAllArticlesAsync();

        return View(model);
    }
}
