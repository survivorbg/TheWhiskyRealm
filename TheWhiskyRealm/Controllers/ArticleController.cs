using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TheWhiskyRealm.Core.Contracts;
using TheWhiskyRealm.Core.Models.Article;
using TheWhiskyRealm.Infrastructure.Data.Enums;

namespace TheWhiskyRealm.Controllers;

public class ArticleController : BaseController
{
    private readonly IArticleService articleService;
    private readonly ICommentService commentService;

    public ArticleController(IArticleService articleService, ICommentService commentService)
    {
        this.articleService = articleService;
        this.commentService = commentService;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var model = await  articleService.GetAllArticlesAsync();

        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        if(await articleService.ArticleExistsAsync(id) == false)
        {
            return NotFound();
        }

        var model = await articleService.GetArticleDetailsAsync(id);
        model.Comments = await  commentService.GetCommentsForArticleAsync(id);
        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        if (await articleService.ArticleExistsAsync(id) == false)
        {
            return NotFound();
        }

        var userId = User.Id();

        if(await articleService.IsTheArticleAuthorAsync(userId,id) == false)
        {
            return Unauthorized();
        }

        var model = await articleService.GetArticleEditAsync(id);

        model.ArticleTypeOptions = Enum.GetNames(typeof(ArticleType));

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(ArticleEditViewModel model)
    {
        if (await articleService.ArticleExistsAsync(model.Id) == false)
        {
            return NotFound();
        }

        var userId = User.Id();

        if (await articleService.IsTheArticleAuthorAsync(userId, model.Id) == false)
        {
            return Unauthorized();
        }

        if (!ModelState.IsValid)
        {
            model.ArticleTypeOptions = Enum.GetNames(typeof(ArticleType));
            return View(model); 
        }

        await articleService.EditArticleAsync(model);

        return RedirectToAction("Details", new {id=model.Id});
    }


    [HttpGet]
    public IActionResult Add()
    {
        var model = new ArticleAddViewModel();
        model.ArticleTypeOptions = Enum.GetNames(typeof(ArticleType));

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Add(ArticleAddViewModel model)
    {

        if (!ModelState.IsValid)
        {
            model.ArticleTypeOptions = Enum.GetNames(typeof(ArticleType));
            return View(model);
        }

        var userId = User.Id();

        var newArticleId = await articleService.AddArticleAsync(model, userId); //TODO check other Add action methods

        return RedirectToAction("Details", "Article", new { id = newArticleId });
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        if (await articleService.ArticleExistsAsync(id) == false)
        {
            return NotFound();
        }

        var userId = User.Id();

        if (await articleService.IsTheArticleAuthorAsync(userId, id) == false)
        {
            return Unauthorized();
        }

        await articleService.DeleteArticleAsync(id);

        return RedirectToAction("Index");
    }

    [HttpGet]
    public async Task<IActionResult> MyArticles()
    {
        var userId = User.Id();
        var model = await articleService.GetUserArticlesAsync(userId);

        return View(model);
    }
}
