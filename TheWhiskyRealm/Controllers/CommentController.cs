using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TheWhiskyRealm.Core.Contracts;
using TheWhiskyRealm.Core.Models.Article;

namespace TheWhiskyRealm.Controllers;

public class CommentController : BaseController
{
    private readonly ICommentService commentService;
    private readonly IArticleService articleService;

    public CommentController(ICommentService commentService, IArticleService articleService)
    {
        this.commentService = commentService;
        this.articleService = articleService;
    }

    [HttpPost]
    public async Task<IActionResult> Add(CommentAddViewModel model)
    {
        if(await articleService.ArticleExistsAsync(model.ArticleId) == false)
        {
            return NotFound();
        }

        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var userId = User.Id();

        await commentService.AddCommentAsync(model,userId);

        return RedirectToAction("Details","Article", new { id = model.ArticleId });
    }
}
