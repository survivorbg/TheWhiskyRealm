using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TheWhiskyRealm.Core.Contracts;
using TheWhiskyRealm.Core.Models.Article;
using TheWhiskyRealm.Infrastructure.Data.Enums;
using static TheWhiskyRealm.Core.Constants.RoleConstants;

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
        var model = await articleService.GetArticleDetailsAsync(id);
        if (model == null)
        {
            return NotFound();
        }

        model.Comments = await  commentService.GetCommentsForArticleAsync(id);

        return View(model);
    }

    [Authorize(Roles = $"{Administrator},{WhiskyExpert}")]
    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var model = await articleService.GetArticleEditAsync(id);

        if (model == null)
        {
            return NotFound();
        }

        var userId = User.Id();

        if(await articleService.IsTheArticleAuthorAsync(userId,id) == false && 
            !User.IsAdmin())
        {
            return Unauthorized();
        }

        model.ArticleTypeOptions = Enum.GetNames(typeof(ArticleType));

        return View(model);
    }

    [Authorize(Roles = $"{Administrator},{WhiskyExpert}")]
    [HttpPost]
    public async Task<IActionResult> Edit(ArticleEditViewModel model)
    {
        if (await articleService.ArticleExistsAsync(model.Id) == false)
        {
            return NotFound();
        }

        var userId = User.Id();

        if (await articleService.IsTheArticleAuthorAsync(userId, model.Id) == false &&
            !User.IsAdmin())
        {
            return Unauthorized();
        }

        if (!ModelState.IsValid)
        {
            model.ArticleTypeOptions = Enum.GetNames(typeof(ArticleType));
            return View(model); 
        }

        await articleService.EditArticleAsync(model);

        return RedirectToAction(nameof(Details), new {id=model.Id});
    }

    [Authorize(Roles = $"{Administrator},{WhiskyExpert}")]
    [HttpGet]
    public IActionResult Add()
    {
        var model = new ArticleAddViewModel();
        model.ArticleTypeOptions = Enum.GetNames(typeof(ArticleType));

        return View(model);
    }

    [Authorize(Roles = $"{Administrator},{WhiskyExpert}")]
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

        return RedirectToAction(nameof(Details), new { id = newArticleId });
    }

    [Authorize(Roles = $"{Administrator},{WhiskyExpert}")]
    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        if (await articleService.ArticleExistsAsync(id) == false)
        {
            return NotFound();
        }

        var userId = User.Id();

        if (await articleService.IsTheArticleAuthorAsync(userId, id) == false &&
            !User.IsAdmin())
        {
            return Unauthorized();
        }

        await articleService.DeleteArticleAsync(id);

        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> MyArticles()
    {
        var userId = User.Id();
        var model = await articleService.GetUserArticlesAsync(userId);

        return View(model);
    }
}
